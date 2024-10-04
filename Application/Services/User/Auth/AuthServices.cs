using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Application.DTO.User.Auth;
using Application.Interfaces.User.Auth;
using Application.Response;
using Domain.Entities.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Application.Services.User.Auth
{
    public class AuthServices : IAuthServices
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;

        public AuthServices(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
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

        public async Task<ServiceResponse<object>> SignIn(SignInDTO SignInModel)
        {
            ApplicationUser? user = await _userManager
                .Users.Where(x => x.PhoneNumber == SignInModel.PhoneNumber)
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
                var result = await _userManager.CheckPasswordAsync(user, SignInModel.Password);
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
    }
}
