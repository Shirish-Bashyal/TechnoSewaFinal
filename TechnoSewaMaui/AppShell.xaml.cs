using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;
using TechnoSewaMaui.Views.Auth.Register;
using TechnoSewaMaui.Views.Auth.SignIn;
using TechnoSewaMaui.Views.Bookings;
using TechnoSewaMaui.Views.Chatbot;
using TechnoSewaMaui.Views.Home;
using TechnoSewaMaui.Views.Notification;
using TechnoSewaMaui.Views.PostProblem;
using TechnoSewaMaui.Views.Profile;
using TechnoSewaMaui.Views.Search;
using TechnoSewaMaui.Views.Technician;

namespace TechnoSewaMaui
{
    public partial class AppShell : Shell
    {
        bool IsConsumer = true;

        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(PhoneNumberPage), typeof(PhoneNumberPage));
            Routing.RegisterRoute(nameof(OtpPage), typeof(OtpPage));
            Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage));

            Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));

            Routing.RegisterRoute(nameof(SearchPage), typeof(SearchPage));
            Routing.RegisterRoute(nameof(BookingsPage), typeof(BookingsPage));
            Routing.RegisterRoute(nameof(ProfilePage), typeof(ProfilePage));
            Routing.RegisterRoute(nameof(LandingPage), typeof(LandingPage));
            Routing.RegisterRoute(nameof(MapPage), typeof(MapPage));
            Routing.RegisterRoute(nameof(PostProblemPage), typeof(PostProblemPage));
            Routing.RegisterRoute(nameof(ChatBotPopup), typeof(ChatBotPopup));

            Routing.RegisterRoute(nameof(NotificationPage), typeof(NotificationPage));
            Routing.RegisterRoute(nameof(SigninPage), typeof(SigninPage));
        }

        public void DecideTabBar(string role)
        {
            if (role == "Consumer")
                ConsumerTabbar.IsVisible = true;
            else
                TechnicianTabbar.IsVisible = true;
        }
    }
}
