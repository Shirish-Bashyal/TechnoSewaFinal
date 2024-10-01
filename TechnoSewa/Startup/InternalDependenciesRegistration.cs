using Application.Interfaces.Data;
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
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddIdentityServices();

            services.AddJwtServices(configuration);

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }
    }
}
