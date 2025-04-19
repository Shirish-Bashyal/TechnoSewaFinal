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
                ApiBaseUrl = "https://b67b-2405-acc0-1504-cce4-9f3-81a8-6562-593b.ngrok-free.app"
            };

            MainPage = new AppShell();
        }
    }
}
