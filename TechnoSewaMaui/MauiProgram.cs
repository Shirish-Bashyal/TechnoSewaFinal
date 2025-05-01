using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using Plugin.LocalNotification;
using Plugin.Maui.Audio;
using Plugin.Maui.Biometric;
using TechnoSewaMaui.Services.Auth.Register;
using TechnoSewaMaui.Services.Auth.SignIn;
using TechnoSewaMaui.Services.Chatbot;
using TechnoSewaMaui.Services.Home;
using TechnoSewaMaui.Services.PostProblem;
using TechnoSewaMaui.ViewModel.Auth.Register;
using TechnoSewaMaui.ViewModel.Auth.SignIn;
using TechnoSewaMaui.ViewModel.Bookings;
using TechnoSewaMaui.ViewModel.Chatbot;
using TechnoSewaMaui.ViewModel.Home;
using TechnoSewaMaui.ViewModel.Notification;
using TechnoSewaMaui.ViewModel.PostProblem;
using TechnoSewaMaui.ViewModel.Profile;
using TechnoSewaMaui.ViewModel.Search;
using TechnoSewaMaui.Views.Auth.Register;
using TechnoSewaMaui.Views.Auth.SignIn;
using TechnoSewaMaui.Views.Bookings;
using TechnoSewaMaui.Views.Chatbot;
using TechnoSewaMaui.Views.Home;
using TechnoSewaMaui.Views.Notification;
using TechnoSewaMaui.Views.PostProblem;
using TechnoSewaMaui.Views.Profile;
using TechnoSewaMaui.Views.Search;

namespace TechnoSewaMaui
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseLocalNotification()
                .UseMauiCommunityToolkit()
                //.ConfigureMauiHandlers(handlers =>
                //{
                //    handlers.AddHandler<ChatBotPopup, CommunityToolkit.Maui.Views.Popup>();
                //})
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
            builder.Services.AddSingleton(AudioManager.Current);

            builder.Services.AddTransient<SignInViewModel>();
            builder.Services.AddTransient<SigninPage>();
            builder.Services.AddTransient<SignInService>();

            builder.Services.AddTransient<OtpPage>();
            builder.Services.AddTransient<OtpViewModel>();

            builder.Services.AddTransient<PhoneNumberPage>();
            builder.Services.AddTransient<PhoneNumberViewModel>();

            builder.Services.AddTransient<RegisterViewModel>();
            builder.Services.AddTransient<RegisterPage>();
            builder.Services.AddTransient<RegisterServices>();

            builder.Services.AddTransient<HomePage>();
            builder.Services.AddTransient<HomeViewModel>();

            builder .Services.AddTransientPopup<ChatBotPopup,ChatbotPopupViewModel>();
            builder.Services.AddTransient<ChatbotService>();

            builder.Services.AddTransient<BookingsPage>();
            builder.Services.AddTransient<BookingsViewModel>();

            builder.Services.AddTransient<NotificationPage>();
            builder.Services.AddTransient<NotificationViewModel>();

            builder.Services.AddTransient<PostProblemPage>();
            builder.Services.AddTransient<PostProblemViewModel>();
            builder.Services.AddTransient<PostProblemService>();

            builder.Services.AddTransient<ProfilePage>();
            builder.Services.AddTransient<ProfileViewModel>();
            builder.Services.AddSingleton(ProfileService.Instance);

            builder.Services.AddTransient<SearchPage>();
            builder.Services.AddTransient<SearchViewModel>();

            builder.Services.AddSingleton<IBiometric>(BiometricAuthenticationService.Default);
            builder.Services.AddSingleton<HttpClient>();
#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
