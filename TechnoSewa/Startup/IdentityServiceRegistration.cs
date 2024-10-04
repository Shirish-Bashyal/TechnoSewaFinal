using System;
using Domain.Entities.User;
using Infrastructure.Data.DbContext;
using Microsoft.AspNetCore.Identity;

namespace TechnoSewa.Startup
{
    public static class IdentityServiceRegistration
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services)
        {
            services
                .AddIdentity<ApplicationUser, IdentityRole>(options => //configure for users
                {
                    options.User.AllowedUserNameCharacters =
                        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+ ";
                    options.User.RequireUniqueEmail = true;
                })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders(); //By including this method, you make it possible to generate and validate these tokens without needing to manually create the token provider infrastructure yourself

            services.Configure<IdentityOptions>(options => //configure for password
            {
                options.Password.RequiredLength = 6;
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.SignIn.RequireConfirmedEmail = false;
            });

            services.Configure<IdentityOptions>(options => //configure for lockout
            {
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15); // Lockout time after too many failed attempts
                options.Lockout.MaxFailedAccessAttempts = 3; // Number of failed attempts before lockout
                options.Lockout.AllowedForNewUsers = true; // Enable lockout for new users,
                // Any newly registered user can be locked out if they exceed the number of allowed failed access attempts
            });

            return services;
        }
    }
}
