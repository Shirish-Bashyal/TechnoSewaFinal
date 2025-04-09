using System;
using System.ComponentModel;
using TechnoSewaMaui.Model;
using TechnoSewaMaui.Services.Home;
using TechnoSewaMaui.ViewModel.Base;

namespace TechnoSewaMaui.ViewModel.Profile
{
    public partial class ProfileViewModel : BaseViewModel
    {
        private readonly ProfileService _profileService;

        // Bind these properties in your XAML
        public string UserName => _profileService.CurrentProfile?.Name ?? "Guest";
        public string PhoneNumber => _profileService.CurrentProfile?.PhoneNumber ?? "Not provided";
        public string Email => _profileService.CurrentProfile?.Email ?? "Not provided";
        public string City => _profileService.CurrentProfile?.City ?? string.Empty;
        public int WardNo => _profileService.CurrentProfile?.WardNo ?? 0;
        public string ToleName => _profileService.CurrentProfile?.ToleName ?? string.Empty;

        public ProfileViewModel()
        {
            _profileService = ProfileService.Instance;

            // Listen for profile changes
            _profileService.PropertyChanged += OnProfileServicePropertyChanged;
        }

        private void OnProfileServicePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ProfileService.CurrentProfile))
            {
                // Notify all properties that depend on the profile
                OnPropertyChanged(nameof(UserName));
                OnPropertyChanged(nameof(PhoneNumber));
                OnPropertyChanged(nameof(Email));
                OnPropertyChanged(nameof(City));
                OnPropertyChanged(nameof(WardNo));
                OnPropertyChanged(nameof(ToleName));
            }
        }

        public void Cleanup()
        {
            _profileService.PropertyChanged -= OnProfileServicePropertyChanged;
        }
    }
}
