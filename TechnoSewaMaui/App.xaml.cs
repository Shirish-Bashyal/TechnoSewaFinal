namespace TechnoSewaMaui
{
    public partial class App : Application
    {
        public static AppSettings Settings { get; private set; }

        public App()
        {
            InitializeComponent();
            Settings = new AppSettings
            {
                ApiBaseUrl = "  https://fe11-103-167-233-215.ngrok-free.app"
            };

            MainPage = new AppShell();
        }
    }
}
