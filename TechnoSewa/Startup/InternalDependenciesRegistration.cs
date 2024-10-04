using Application.Interfaces.Data;
using Application.Interfaces.User.Auth;
using Application.Interfaces.User.Role;
using Application.Services.User.Auth;
using Application.Services.User.Role;
using Infrastructure.Data.Repository;

namespace TechnoSewa.Startup
{
    public static class InternalDependenciesRegistration
    {
        public static void AddInternalDependencies(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            services.AddScoped<IRoleServices, RoleServices>();

            services.AddScoped<IAuthServices, AuthServices>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddIdentityServices();

            services.AddJwtServices(configuration);

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }
    }
}
