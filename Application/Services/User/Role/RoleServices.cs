using Application.Interfaces.User.Role;
using Application.Response;
using Microsoft.AspNetCore.Identity;

namespace Application.Services.User.Role
{
    public class RoleServices : IRoleServices
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleServices(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
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
