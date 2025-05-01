using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO.Technician;
using Application.Interfaces.Data;
using Application.Interfaces.Technician;
using Application.Interfaces.User.Role;
using Application.Response;
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

        public TechnicianService(
            IUnitOfWork uow,
            UserManager<ApplicationUser> userManager,
            IRoleServices roleServices
        )
        {
            _uow = uow;
            _userManager = userManager;
            _roleServices = roleServices;
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
                    User = user
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
    }
}
