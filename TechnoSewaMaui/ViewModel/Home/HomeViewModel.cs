using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Android.App;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using TechnoSewaMaui.Services.Home;
using TechnoSewaMaui.ViewModel.Base;
using TechnoSewaMaui.ViewModel.Chatbot;
using TechnoSewaMaui.Views.Chatbot;
using TechnoSewaMaui.Views.Home;
using TechnoSewaMaui.Views.Notification;
using TechnoSewaMaui.Views.PostProblem;

namespace TechnoSewaMaui.ViewModel.Home
{
    public partial class HomeViewModel : BaseViewModel
    {
        public Command OnNotificationTapped { get; }

        private readonly IPopupService _popupService;
        private readonly ChatbotPopupViewModel _chatbotPopupViewModel;
        public Command OnPageMount { get; }
        public Command OnPersonnelTapped { get; }
        public Command OnLocationTapped { get; }
        public Command OnChatbotTapped { get; }
        public string[] Locations { get; } = { "Kathmandu", "Pokhara", "Biratnagar" };

        [ObservableProperty]
        private ObservableCollection<string> imageList = [];

        [ObservableProperty]
        public string selectedLocation;

        [ObservableProperty]
        private int currentIndex;

        private System.Timers.Timer _timer;
        private readonly ProfileService _profileService = ProfileService.Instance;

        public HomeViewModel(IPopupService popupService, ChatbotPopupViewModel chatbotPopupViewModel)
        {
            _popupService = popupService;
            _chatbotPopupViewModel = chatbotPopupViewModel;
            OnPageMount = new Command(async () => await PageMount());
            Task.Run(async () => await ImageCarousel());
            OnNotificationTapped = new Command(async () => await NotificationTapped());
            OnPersonnelTapped = new Command(async () => await PersonnelTapped());
            OnLocationTapped = new Command(async () => await LocationTapped());
            OnChatbotTapped = new Command(async() => await ChatbotTapped());
        }

        public async Task ChatbotTapped()
        {
            var popup = new ChatBotPopup(_chatbotPopupViewModel);
            await _popupService.ShowPopupAsync<ChatbotPopupViewModel>();
            //await Shell.Current.GoToAsync(nameof(ChatBotPopup));
        }
        public async Task PageMount()
        {
            await _profileService.GetUserProfile();
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
