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
                ApiBaseUrl = "https://7778-103-134-219-181.ngrok-free.app"
            };

            MainPage = new AppShell();
        }
    }
}
