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
                ApiBaseUrl = "https://1646-103-134-219-146.ngrok-free.app"
            };

            MainPage = new AppShell();
        }
    }
}
