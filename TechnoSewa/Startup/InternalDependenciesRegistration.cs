using Application.Helpers.LLM;
using Application.InMemoryCache;
using Application.Interfaces;
using Application.Interfaces.Bookings;
using Application.Interfaces.Chatbot;
using Application.Interfaces.Data;
using Application.Interfaces.LLM;
using Application.Interfaces.Review;
using Application.Interfaces.Technician;
using Application.Interfaces.User.Auth;
using Application.Interfaces.User.Consumer;
using Application.Interfaces.User.Role;
using Application.Services;
using Application.Services.Bookings;
using Application.Services.Chatbot;
using Application.Services.Technician;
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
            services.AddScoped<IChatbotService, ChatbotService>();
            services.AddSingleton<UserChatDb>();
            services.AddScoped<IBidService, BidService>();
            services.AddScoped<IReviewServices, ReviewServices>();

            services.AddScoped<ICategoryServices, CategoryServices>();
            services.AddScoped<IBookingService, BookingService>();

            services.AddScoped<ITechnicianService, TechnicianService>();

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
