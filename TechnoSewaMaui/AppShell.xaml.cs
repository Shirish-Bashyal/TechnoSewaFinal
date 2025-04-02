using TechnoSewaMaui.Views.Auth.Register;
using TechnoSewaMaui.Views.Auth.SignIn;
using TechnoSewaMaui.Views.Home;
using TechnoSewaMaui.Views.Notification;
using TechnoSewaMaui.Views.PostProblem;

namespace TechnoSewaMaui
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(SigninPage), typeof(SigninPage));
            Routing.RegisterRoute(nameof(PhoneNumberPage), typeof(PhoneNumberPage));
            Routing.RegisterRoute(nameof(OtpPage), typeof(OtpPage));
            Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage));
            Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
            Routing.RegisterRoute(nameof(NotificationPage), typeof(NotificationPage));
            Routing.RegisterRoute(nameof(PostProblemPage), typeof(PostProblemPage));
        }
    }
}
