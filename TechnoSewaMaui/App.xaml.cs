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
                ApiBaseUrl = "https://4079-103-134-216-114.ngrok-free.app"
            };

            MainPage = new AppShell();
        }
    }
}
