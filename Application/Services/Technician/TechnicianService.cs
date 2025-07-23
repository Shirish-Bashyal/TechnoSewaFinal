using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Application.DTO.Review;
using Application.DTO.Technician;
using Application.Helper;
using Application.Helpers.MachineLearningModel;
using Application.Interfaces.Data;
using Application.Interfaces.Payment;
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
        private readonly IPaymentServics _paymentServics;

        public TechnicianService(
            IUnitOfWork uow,
            UserManager<ApplicationUser> userManager,
            IRoleServices roleServices,
            IReviewServices reviewServices,
            IPaymentServics paymentServics
        )
        {
            _uow = uow;
            _userManager = userManager;
            _roleServices = roleServices;
            _reviewServices = reviewServices;
            _paymentServics = paymentServics;
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
                    IsVerified = false,
                };
                //wait for the admin to verify for the role change
                await _uow.AsyncRepositories<Domain.Entities.User.Technician>().AddAsync(tech);
                var result = await _uow.Save();
                if (result > 0)
                {
                    //var role = await _roleServices.ChangeRole(userId, "Technician");

                    //if (role.Success)
                    //{
                    //    return role;
                    //}
                    //else
                    //{
                    //    await _uow.AsyncRepositories<Domain.Entities.User.Technician>()
                    //        .DeleteAsync(technicianData);
                    //    await _uow.Save();
                    //    return role;
                    //}
                    return new ServiceResponse<object>
                    {
                        Message = "Wait for admin to verify.",
                        Success = true,
                    };
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

        public async Task<ServiceResponse<object>> GetAll()
        {
            var includes = new Expression<Func<Domain.Entities.User.Technician, object>>[]
            {
                x => x.User,
                x => x.User.Address,
                x => x.User.Address.City,
            };
            var technician = await _uow.AsyncRepositories<Domain.Entities.User.Technician>()
                .GetWithInclude(includes);
            if (technician == null)
            {
                return new ServiceResponse<object>
                {
                    Success = false,
                    Message = "Technician not found"
                };
            }
            var result = technician.Select(x => new GetTechnicianAllDetails
            {
                TechnicianId = x.Id,
                SecondPhoneNumber = x.SecondPhoneNumber,
                IsVerified = x.IsVerified,
                Name = x.User.UserName,
                PhoneNumber = x.User.PhoneNumber,
                Address =
                    $"{x.User.Address.City.Name}-{x.User.Address.WardNo},{x.User.Address.ToleName}",
            });

            return new ServiceResponse<object> { Success = true, Data = result };
        }

        public async Task<ServiceResponse<List<GetTechnicianDetailsDTO>>> GetByFilter(
            GetByFilterDTO model
        )
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
                            x.IsVerified
                            && (
                                x.TechnicianBookings == null
                                || !x.TechnicianBookings.Any(b =>
                                    b.ServiceDate == model.Date
                                    && b.TimeFrame.Id == (int)model.TimeFrameEnum
                                )
                            )
                    );
            if (availableTechnician.Any())
            {
                var unblockedTechnicians = new List<Domain.Entities.User.Technician>();

                //get the technician whose comission limit is not reached
                foreach (var tech in availableTechnician)
                {
                    var isLimitReached = await _paymentServics.CheckLimitReached(tech.Id);
                    if (!isLimitReached)
                    {
                        unblockedTechnicians.Add(tech);
                    }
                }

                var result = unblockedTechnicians
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

                foreach (var tech in result)
                {
                    var reviewResponse = await _reviewServices.GetForTechnician(tech.TechnicianId);
                    if (reviewResponse.Data != null)
                    {
                        tech.Reviews = reviewResponse.Data;
                    }
                }

                //var predictorInput = result
                //    .Select(x =>
                //        (
                //            id: x.TechnicianId,
                //            proximityKm: (float)x.Distance,
                //            avgRating: (float)x.Reviews.AverageRating
                //        )
                //    )
                //    .ToList();

                //var predictorOutput = LightGBMPredictor.Predict(predictorInput);

                return new ServiceResponse<List<GetTechnicianDetailsDTO>>
                {
                    Success = true,
                    Data = result
                };
            }
            else
            {
                return new ServiceResponse<List<GetTechnicianDetailsDTO>>
                {
                    Success = true,
                    Message = "No technician available"
                };
            }
        }

        public async Task<ServiceResponse<object>> GetById(int TechnicianId)
        {
            var includes = new Expression<Func<Domain.Entities.User.Technician, object>>[]
            {
                x => x.User,
                x => x.User.Address,
                x => x.User.Address.City,
            };
            var technician = await _uow.AsyncRepositories<Domain.Entities.User.Technician>()
                .GetWithIncludeAndFilter(includes, x => x.Id == TechnicianId);
            if (technician == null)
            {
                return new ServiceResponse<object>
                {
                    Success = false,
                    Message = "Technician not found"
                };
            }
            var result = new GetTechnicianAllDetails
            {
                TechnicianId = technician.Id,
                SecondPhoneNumber = technician.SecondPhoneNumber,
                IsVerified = technician.IsVerified,
                Name = technician.User.UserName,
                PhoneNumber = technician.User.PhoneNumber,
                Address =
                    $"{technician.User.Address.City.Name}-{technician.User.Address.WardNo},{technician.User.Address.ToleName}",
            };

            return new ServiceResponse<object> { Success = true, Data = result };
        }
    }
}
