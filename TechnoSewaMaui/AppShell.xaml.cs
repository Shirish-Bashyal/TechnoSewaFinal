using TechnoSewaMaui.Views.Auth.SignIn;

namespace TechnoSewaMaui
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(SigninPage), typeof(SigninPage));
        }
    }
}
