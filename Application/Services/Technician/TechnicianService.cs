using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Application.DTO.Review;
using Application.DTO.Technician;
using Application.Helper;
using Application.Interfaces.Data;
using Application.Interfaces.Review;
using Application.Interfaces.Technician;
using Application.Interfaces.User.Role;
using Application.Response;
using Domain.Entities.Application.Bookings;
using Domain.Entities.User;
using Domain.Entities.User.PostDetails;
using Microsoft.AspNetCore.Identity;

namespace Application.Services.Technician
{
    public class TechnicianService : ITechnicianService
    {
        private readonly IUnitOfWork _uow;
        private readonly IRoleServices _roleServices;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IReviewServices _reviewServices;

        public TechnicianService(
            IUnitOfWork uow,
            UserManager<ApplicationUser> userManager,
            IRoleServices roleServices,
            IReviewServices reviewServices
        )
        {
            _uow = uow;
            _userManager = userManager;
            _roleServices = roleServices;
            _reviewServices = reviewServices;
        }

        public async Task<ServiceResponse<object>> BecomeTechnician(
            BecomeTechnicianDTO model,
            string userId
        )
        {
            ApplicationUser? user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var tech = new Domain.Entities.User.Technician
                {
                    SecondPhoneNumber = model.SecondPhoneNumber,
                    User = user,
                    Latitude = model.Latitude,
                    Longitude = model.Longitude,
                };
                //change the role of user
                var technicianData = await _uow.AsyncRepositories<Domain.Entities.User.Technician>()
                    .AddAsync(tech);
                var result = await _uow.Save();
                if (result > 0)
                {
                    var role = await _roleServices.ChangeRole(userId, "Technician");

                    if (role.Success)
                    {
                        return role;
                    }
                    else
                    {
                        await _uow.AsyncRepositories<Domain.Entities.User.Technician>()
                            .DeleteAsync(technicianData);
                        await _uow.Save();
                        return role;
                    }
                }
                else
                {
                    return new ServiceResponse<object>
                    {
                        Message = "operation error",
                        Success = false,
                    };
                }
            }
            else
            {
                return new ServiceResponse<object> { Message = "User not found", Success = false, };
            }
        }

        public async Task<ServiceResponse<object>> GetByFilter(GetByFilterDTO model)
        {
            var includes = new Expression<Func<Domain.Entities.User.Technician, object>>[]
            {
                x => x.User
            };

            var availableTechnician =
                await _uow.AsyncRepositories<Domain.Entities.User.Technician>()
                    .GetListWithIncludeAndFilter(
                        includes,
                        x =>
                            !x.TechnicianBookings.Any(b =>
                                b.ServiceDate == model.Date && b.TimeFrame.Id == model.TimeFrameEnum
                            )
                    );

            //rank these technicians based on the locations


            var result = availableTechnician
                .Select(x => new GetTechnicianDetailsDTO
                {
                    TechnicianId = x.Id,
                    Name = x.User.UserName,
                    Distance = HaversineAlgo.Haversine(
                        model.Latitude,
                        model.Longitude,
                        x.Latitude,
                        x.Longitude
                    ),
                    //PhoneNumber = x.User.PhoneNumber,
                    Reviews = new GetReviewDTO()
                })
                .OrderBy(x => x.Distance)
                .Take(10)
                .ToList(); //order by distance in kilometers

            var reviewTasks = result.Select(async technician =>
            {
                var reviewResponse = await _reviewServices.GetForTechnician(
                    technician.TechnicianId
                );
                if (reviewResponse.Data != null)
                {
                    technician.Reviews = reviewResponse.Data;
                }
            });

            await Task.WhenAll(reviewTasks); // Await all in parallel

            return new ServiceResponse<object> { Success = true, Data = result };
        }
    }
}
