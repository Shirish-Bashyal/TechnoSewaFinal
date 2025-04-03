using Application.DTO.User.Consumer;
using Application.Interfaces.Data;
using Application.Interfaces.User.Consumer;
using Application.Response;
using Domain.Entities.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.User.Consumer
{
    public class ProfileService : IProfileService
    {
        private readonly IUnitOfWork _uow;
        private readonly UserManager<ApplicationUser> _userManager;

        public ProfileService(IUnitOfWork uow, UserManager<ApplicationUser> userManager)
        {
            _uow = uow;
            _userManager = userManager;
        }

        public async Task<ServiceResponse<object>> ConsumerProfile(string userId)
        {
            var user = await _userManager
                .Users.Include(x => x.Address)
                .ThenInclude(x => x.City)
                .Where(x => x.Id == userId)
                .FirstAsync();

            if (user == null)
            {
                return new ServiceResponse<object>
                {
                    Success = false,
                    Message = "user doesnot exists"
                };
            }
            else
            {
                var roles = await _userManager.GetRolesAsync(user);

                var result = new ConsumerProfileDTO
                {
                    Name = user.UserName,
                    PhoneNumber = user.PhoneNumber,
                    Email = user.Email,
                    City = user.Address.City.Name,
                    ToleName = user.Address.ToleName,
                    WardNo = user.Address.WardNo,
                    Role = roles.First()
                };

                return new ServiceResponse<object> { Success = true, Data = result };
            }
        }
    }
}
