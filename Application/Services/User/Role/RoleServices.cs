using Application.Interfaces.User.Role;
using Application.Response;
using Domain.Entities.User;
using Microsoft.AspNetCore.Identity;

namespace Application.Services.User.Role
{
    public class RoleServices : IRoleServices
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        private readonly UserManager<ApplicationUser> _userManager;

        public RoleServices(
            RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager
        )
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<ServiceResponse<object>> ChangeRole(string userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return new ServiceResponse<object>
                {
                    Success = false,
                    Message = "user doesnot exist"
                };
            }

            bool roleExists = await _roleManager.RoleExistsAsync(roleName);
            if (roleExists)
            {
                var currentRoles = await _userManager.GetRolesAsync(user);

                if (currentRoles.Contains(roleName))
                {
                    return new ServiceResponse<object>
                    {
                        Success = false,
                        Message = $"you are already {roleName}"
                    };
                }

                await _userManager.RemoveFromRolesAsync(user, currentRoles);
                await _userManager.AddToRoleAsync(user, roleName);

                return new ServiceResponse<object>
                {
                    Success = true,
                    Message = $"Role updated to {roleName}"
                };
            }
            return new ServiceResponse<object>
            {
                Success = false,
                Message = $"{roleName} role doesnot exists"
            };
        }

        public async Task<ServiceResponse<object>> CreateRole(string roleName)
        {
            bool roleExists = await _roleManager.RoleExistsAsync(roleName);
            if (roleExists)
            {
                return new ServiceResponse<object>
                {
                    Message = $"{roleName} already exists",

                    Success = false,
                };
            }

            var role = new IdentityRole { Name = roleName };

            var createRole = await _roleManager.CreateAsync(role);
            if (createRole.Succeeded)
            {
                return new ServiceResponse<object>
                {
                    Success = true,
                    Message = $"{roleName} has been created successfully"
                };
            }
            else
            {
                // Handle failure case
                return new ServiceResponse<object>
                {
                    Success = false,
                    Message = $"Failed to create role: {roleName}"
                };
            }
        }
    }
}
