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
        public Command OnLocationTapped { get; }
        public string[] Locations { get; } = { "Kathmandu", "Pokhara", "Biratnagar" };

        [ObservableProperty]
        private ObservableCollection<string> imageList = [];

        [ObservableProperty]
        public string selectedLocation;

        [ObservableProperty]
        private int currentIndex;

        private System.Timers.Timer _timer;

        public HomeViewModel()
        {
            Task.Run(async () => await ImageCarousel());
            OnNotificationTapped = new Command(async () => await NotificationTapped());
            OnPersonnelTapped = new Command(async () => await PersonnelTapped());
            OnLocationTapped = new Command(async () => await LocationTapped());
        }

        public async Task NotificationTapped()
        {
            await Shell.Current.GoToAsync($"{nameof(NotificationPage)}");
        }

        public async Task LocationTapped()
        {
            var result = await Shell.Current.DisplayActionSheet(
                "Select Location",
                "Cancel",
                null,
                Locations
            );

            if (!string.IsNullOrWhiteSpace(result) && result != "Cancel")
            {
                SelectedLocation = result;
            }
        }

        public Color GetLocationColor(string location)
        {
            return location == SelectedLocation ? Colors.LightBlue : Colors.Transparent;
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
