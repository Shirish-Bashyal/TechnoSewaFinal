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
                ApiBaseUrl = "https://1164-2405-acc0-1504-cce4-b95d-93d2-4515-306c.ngrok-free.app"
            };

            MainPage = new AppShell();
        }
    }
}
