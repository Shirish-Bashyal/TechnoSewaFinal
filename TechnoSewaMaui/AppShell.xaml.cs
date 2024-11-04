using TechnoSewaMaui.Views.Auth.Register;
using TechnoSewaMaui.Views.Auth.SignIn;

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
        }
    }
}
