using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using Plugin.LocalNotification;
using Plugin.Maui.Biometric;
using TechnoSewaMaui.Services.Auth.Register;
using TechnoSewaMaui.Services.Auth.SignIn;
using TechnoSewaMaui.ViewModel.Auth.Register;
using TechnoSewaMaui.ViewModel.Auth.SignIn;
using TechnoSewaMaui.Views.Auth.Register;
using TechnoSewaMaui.Views.Auth.SignIn;

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
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

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

            builder.Services.AddSingleton<IBiometric>(BiometricAuthenticationService.Default);
            builder.Services.AddSingleton<HttpClient>();
#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
