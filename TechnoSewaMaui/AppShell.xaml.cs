using TechnoSewaMaui.Views.Auth.Register;
using TechnoSewaMaui.Views.Auth.SignIn;
using TechnoSewaMaui.Views.Chatbot;
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
            Routing.RegisterRoute(nameof(MapPage), typeof(MapPage));
            Routing.RegisterRoute(nameof(PostProblemPage), typeof(PostProblemPage));
            Routing.RegisterRoute(nameof(ChatBotPopup), typeof(ChatBotPopup));


            // If you have other modal pages:
            Routing.RegisterRoute(nameof(NotificationPage), typeof(NotificationPage));
        }
    }
}
