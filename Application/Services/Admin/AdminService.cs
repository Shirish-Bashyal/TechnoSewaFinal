using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Constants.Enums;
using Application.DTO.Admin;
using Application.Hubs.InMemoryDB;
using Application.Interfaces.Admin;
using Application.Interfaces.Data;
using Application.Interfaces.User.Role;
using Application.Response;
using Domain.Entities.Application.Bookings;
using Domain.Entities.Application.Payment;
using Domain.Entities.User;
using Microsoft.AspNetCore.Identity;

namespace Application.Services.Admin
{
    public class AdminService : IAdminService
    {
        private readonly IUnitOfWork _uow;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IRoleServices _roleManager;
        private readonly UserConnectionDb _connectionDb;

        public AdminService(
            IRoleServices roleManager,
            UserManager<ApplicationUser> userManager,
            IUnitOfWork uow,
            UserConnectionDb connectionDb
        )
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _uow = uow;
            _connectionDb = connectionDb;
        }

        public async Task<ServiceResponse<object>> GetOverview()
        {
            var consumers = await _userManager.GetUsersInRoleAsync("Consumer");
            var totalConsumers = consumers.Count;
            var technicians = await _userManager.GetUsersInRoleAsync("Technician");
            var totalTechnicians = technicians.Count;

            var totalUsers = totalConsumers + totalTechnicians;
            int totalActiveUsers = 0;
            int totalActiveConsumers = 0;
            int totalActiveTechnicians = 0;

            if (!_connectionDb.connections.IsEmpty)
            {
                foreach (var connection in _connectionDb.connections)
                {
                    var userId = connection.Key;
                    if (consumers.Any(x => x.Id == userId))
                    {
                        totalActiveConsumers++;
                    }
                    else if (technicians.Any(x => x.Id == userId))
                    {
                        totalActiveTechnicians++;
                    }
                    else
                    {
                        continue;
                    }
                }

                totalActiveUsers = totalActiveConsumers + totalTechnicians;
            }
            int totalPendingBookings = 0;
            int totalActiveBookings = 0;
            int totalCompletedBookings = 0;
            int totalBookings = 0;
            var bookings = await _uow.AsyncRepositories<Booking>().GetAllAsync();
            if (bookings.Any())
            {
                totalPendingBookings = bookings.Count(x => x.Status == (int)PostStatusEnum.Pending);

                totalActiveBookings = bookings.Count(x => x.Status == (int)PostStatusEnum.Booked);

                totalCompletedBookings = bookings.Count(x =>
                    x.Status == (int)PostStatusEnum.Completed
                );
                totalBookings = totalActiveBookings + totalCompletedBookings + totalPendingBookings;
            }
            double totalRevenue = 0;
            var transaction = await _uow.AsyncRepositories<TransactionDetail>().GetAllAsync();
            if (transaction.Any())
            {
                totalRevenue = transaction.Sum(x => x.Amount);
            }

            var result = new DashboardDTO
            {
                TotalActiveBookings = totalActiveBookings,
                TotalCompletedBookings = totalCompletedBookings,
                TotalRevenue = totalRevenue,
                TotalBookings = totalBookings,
                TotalPendingBookings = totalPendingBookings,
                TotalActiveConsumers = totalActiveConsumers,
                TotalActiveTechnicians = totalActiveTechnicians,
                TotalActiveUsers = totalActiveUsers,
                TotalConsumers = totalConsumers,
                TotalTechnicians = totalTechnicians,
                TotalUsers = totalUsers,
            };

            return new ServiceResponse<object> { Data = result, Success = true, };
        }

        public async Task<ServiceResponse<object>> VerifyTechnician(int TechnicianId)
        {
            var technician = await _uow.AsyncRepositories<Domain.Entities.User.Technician>()
                .GetByPrimaryKey(TechnicianId);
            if (technician == null)
            {
                return new ServiceResponse<object>
                {
                    Success = false,
                    Message = "Technician not found"
                };
            }

            technician.IsVerified = true;
            await _uow.AsyncRepositories<Domain.Entities.User.Technician>().UpdateAsync(technician);
            var result = await _uow.Save();
            if (result > 0)
            {
                var role = await _roleManager.ChangeRole(technician.UserId, "Technician");
                if (role.Success)
                {
                    return role;
                }
                else
                {
                    technician.IsVerified = false;
                    await _uow.AsyncRepositories<Domain.Entities.User.Technician>()
                        .UpdateAsync(technician);
                    await _uow.Save();
                    return role;
                }
            }

            return new ServiceResponse<object> { Success = false, Message = "Operation failed" };
        }
    }
}
