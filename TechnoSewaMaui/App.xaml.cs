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
                ApiBaseUrl = "https://63e4-103-134-219-129.ngrok-free.app"
            };

            MainPage = new AppShell();
        }
    }
}
