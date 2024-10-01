using System.Net;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace TechnoSewa.Startup
{
    public static class JwtServiceRegistration
    {
        public static IServiceCollection AddJwtServices(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            services
                .AddAuthentication(options =>
                {
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.RequireHttpsMetadata = false; //change to true for production
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidIssuer = configuration["JWT:ValidIssuer"],
                        ValidAudience = configuration["JWT:ValidAudience"],
                        RequireExpirationTime = true,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(configuration["JWT:Secret"])
                        ),
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true
                    };
                    options.Events = new JwtBearerEvents
                    {
                        //Handles extracting the JWT token from an HttpOnly cookie named "MyAuthValue"
                        OnMessageReceived = context =>
                        {
                            // Look for the token in the HttpOnly cookie named "MyAuthValue"
                            if (
                                context.Request.Cookies.TryGetValue(
                                    "MyAuthValue",
                                    out string jwtToken
                                )
                            )
                            {
                                context.Token = jwtToken;
                            }
                            return Task.CompletedTask;
                        },

                        //Triggered if the JWT authentication fails (e.g., invalid token, expired token).

                        OnAuthenticationFailed = context =>
                        {
                            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                            context.Response.ContentType = "application/json";
                            var problemDetails = new ProblemDetails
                            {
                                Status = (int)HttpStatusCode.Unauthorized,
                                Title = "Unauthorized",

                                Detail = "Invalid token",
                                Instance =
                                    $"{context.Request.Scheme}://{context.Request.Host}{context.Request.Path}{context.Request.QueryString}"
                            };
                            return context.Response.WriteAsync(
                                JsonSerializer.Serialize(problemDetails)
                            );
                        },

                        //Triggered when a request requires authentication, but the user is not authenticated.
                        OnChallenge = context =>
                        {
                            context.HandleResponse();
                            if (!context.Response.HasStarted)
                            {
                                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                                context.Response.ContentType = "application/json";
                                var problemDetails = new ProblemDetails
                                {
                                    Status = (int)HttpStatusCode.Unauthorized,
                                    Title = "Unauthorized",
                                    Detail = "You are not authorized to access this endpoint.",
                                    Instance =
                                        $"{context.Request.Scheme}://{context.Request.Host}{context.Request.Path}{context.Request.QueryString}"
                                };
                                return context.Response.WriteAsync(
                                    JsonSerializer.Serialize(problemDetails)
                                );
                            }
                            return Task.CompletedTask;
                        }
                    };
                });

            return services;
        }
    }
}
