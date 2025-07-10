using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO.User.Consumer;
using Application.Interfaces.Data;
using Application.Interfaces.User.Consumer;
using Application.Response;
using Domain.Entities.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.User.Consumer
{
    public class ConsumerService : IConsumerService
    {
        private readonly IUnitOfWork _uow;
        private readonly UserManager<ApplicationUser> _userManager;

        public ConsumerService(IUnitOfWork uow, UserManager<ApplicationUser> userManager)
        {
            _uow = uow;
            _userManager = userManager;
        }

        public async Task<ServiceResponse<object>> GetAll()
        {
            var consumers = await _userManager.GetUsersInRoleAsync("Consumer");
            if (consumers == null || !consumers.Any())
            {
                return new ServiceResponse<object>
                {
                    Success = true,
                    Message = "No Consumers Found"
                };
            }

            var result = consumers
                .Select(x => new GetAllConsumersDTO
                {
                    Id = x.Id,
                    Name = x.UserName,
                    Address = $"{x.Address.City.Name}-{x.Address.WardNo},{x.Address.ToleName}",
                    Phone = x.PhoneNumber
                })
                .ToList();

            return new ServiceResponse<object> { Success = true, Data = result, };
        }
    }
}
