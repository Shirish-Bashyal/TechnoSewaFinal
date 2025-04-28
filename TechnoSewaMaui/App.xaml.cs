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
                ApiBaseUrl = "https://831f-103-134-219-179.ngrok-free.app"
            };

            MainPage = new AppShell();
        }
    }
}
