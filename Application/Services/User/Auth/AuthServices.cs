using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Application.Constants.Enums;
using Application.DTO.User.Auth;
using Application.Interfaces.Data;
using Application.Interfaces.User.Auth;
using Application.Response;
using Domain.Entities.User;
using Domain.Entities.User.AddressDetails;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Application.Services.User.Auth
{
    public class AuthServices : IAuthServices
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _uow;

        public AuthServices(
            UserManager<ApplicationUser> userManager,
            IConfiguration configuration,
            IUnitOfWork uow,
            RoleManager<IdentityRole> roleManager
        )
        {
            _userManager = userManager;
            _configuration = configuration;
            _uow = uow;
            _roleManager = roleManager;
        }

        //generate JWT Token
        private string GenerateNewJsonWebToken(List<Claim> claims)
        {
            var authSecret = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["JWT:Secret"])
            );

            var tokenObject = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddDays(30),
                claims: claims,
                signingCredentials: new SigningCredentials(
                    authSecret,
                    SecurityAlgorithms.HmacSha256
                )
            );

            string token = new JwtSecurityTokenHandler().WriteToken(tokenObject);

            return token;
        }

        public async Task<ServiceResponse<object>> SignIn(SignInDTO signInModel)
        {
            ApplicationUser? user = await _userManager
                .Users.Where(x => x.PhoneNumber == signInModel.PhoneNumber)
                .FirstOrDefaultAsync();

            if (user != null)
            {
                if (await _userManager.IsLockedOutAsync(user)) //will lock after 3 failed attempts
                {
                    return new ServiceResponse<object>
                    {
                        Success = false,
                        Message = "Your account is locked. Please try again later."
                    };
                }
                var result = await _userManager.CheckPasswordAsync(user, signInModel.Password);
                if (result)
                {
                    await _userManager.ResetAccessFailedCountAsync(user);

                    //create a jwt and return to controller

                    var userRoles = await _userManager.GetRolesAsync(user);

                    var authClaims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Id),
                        new Claim("JWTID", Guid.NewGuid().ToString()),
                    };

                    foreach (var userRole in userRoles)
                    {
                        authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                    }

                    var token = GenerateNewJsonWebToken(authClaims);

                    return new ServiceResponse<object> { Success = true, Data = token };
                }

                await _userManager.AccessFailedAsync(user);

                return new ServiceResponse<object>
                {
                    Success = false,
                    Message = "Invalid Password"
                };
            }
            return new ServiceResponse<object>
            {
                Success = false,
                Message = "Phone Number Doesnot Exists"
            };
        }

        public Task<ServiceResponse<object>> SignOut()
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<object>> Register(RegisterUserDTO registerModel)
        {
            City? city = await _uow.AsyncRepositories<City>()
                .GetSingleBySpec(x => x.Name == registerModel.City);
            if (city == null)
            {
                return new ServiceResponse<object>
                {
                    Success = false,
                    Message = $"{registerModel.City} doesnot exists"
                };
            }

            var user = await _userManager.FindByEmailAsync(registerModel.Email);

            if (user != null)
            {
                return new ServiceResponse<object>
                {
                    Success = false,
                    Message = $"{registerModel.Email} already exists"
                };
            }
            var doesPhoneExists = await _userManager.Users.AnyAsync(x =>
                x.PhoneNumber == registerModel.PhoneNumber
            );
            if (doesPhoneExists)
            {
                return new ServiceResponse<object>
                {
                    Success = false,
                    Message = $"{registerModel.PhoneNumber} already exists"
                };
            }

            var identityUser = new ApplicationUser
            {
                Email = registerModel.Email,
                NormalizedEmail = registerModel.Email.ToUpperInvariant(),
                UserName = registerModel.FullName,
                NormalizedUserName = registerModel.FullName.ToUpperInvariant(),
                PhoneNumber = registerModel.PhoneNumber,
                PhoneNumberConfirmed = true,
                Address = new Address
                {
                    City = city,
                    ToleName = registerModel.ToleName,
                    WardNo = registerModel.WardNo,
                }
            };

            var result = await _userManager.CreateAsync(identityUser, registerModel.Password);
            if (!result.Succeeded)
            {
                return new ServiceResponse<object>
                {
                    Success = false,
                    Message = "Registration Failed"
                };
            }

            await _userManager.AddToRoleAsync(identityUser, RoleEnum.Consumer.ToString());

            return new ServiceResponse<object> { Success = true, Message = "User Registered" };
        }
    }
}
