using Application.Helpers.LLM;
using Application.Interfaces.Data;
using Application.Interfaces.LLM;
using Application.Interfaces.User.Auth;
using Application.Interfaces.User.Consumer;
using Application.Interfaces.User.Role;
using Application.Services.Chatbot;
using Application.Services.User.Auth;
using Application.Services.User.Consumer;
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
            services.AddScoped<IQuestionResponseService, QuestionResponseService>();
            services.AddScoped<ILLMFormatter, LLMFormatter>();
            services.AddScoped<ITextTokenizer, TextTokenizer>();
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<IProfileService, ProfileService>();
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
