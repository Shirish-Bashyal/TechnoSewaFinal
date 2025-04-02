using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using TechnoSewaMaui.ViewModel.Base;
using TechnoSewaMaui.Views.Home;
using TechnoSewaMaui.Views.Notification;
using TechnoSewaMaui.Views.PostProblem;

namespace TechnoSewaMaui.ViewModel.Home
{
    public partial class HomeViewModel : BaseViewModel
    {
        public Command OnNotificationTapped { get; }
        public Command OnPersonnelTapped { get; }

        [ObservableProperty]
        private ObservableCollection<string> imageList = [];

        [ObservableProperty]
        private int currentIndex;

        private System.Timers.Timer _timer;

        public HomeViewModel()
        {
            Task.Run(async () => await ImageCarousel());
            OnNotificationTapped = new Command(async () => await NotificationTapped());
            OnPersonnelTapped = new Command(async () => await PersonnelTapped());
        }

        public async Task NotificationTapped()
        {
            await Shell.Current.GoToAsync($"{nameof(NotificationPage)}");
        }

        public async Task PersonnelTapped()
        {
            await Shell.Current.GoToAsync(nameof(PostProblemPage));
        }

        public async Task ImageCarousel()
        {
            // Load images from the Resources/Images folder
            ImageList = ["dotnet_bot.png", "secure.png"];

            StartAutoSwipe();
        }

        private void StartAutoSwipe()
        {
            _timer = new System.Timers.Timer(4000);
            _timer.Elapsed += (s, e) =>
            {
                if (ImageList.Count == 0)
                    return;
                CurrentIndex = (CurrentIndex + 1) % ImageList.Count;
            };
            _timer.AutoReset = true;
            _timer.Start();
        }
    }
}
