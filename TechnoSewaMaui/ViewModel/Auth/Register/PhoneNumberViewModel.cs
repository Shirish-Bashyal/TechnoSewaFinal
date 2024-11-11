using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.LocalNotification;
using TechnoSewaMaui.ViewModel.Base;
using TechnoSewaMaui.Views.Auth.Register;
using TechnoSewaMaui.Views.Auth.SignIn;

namespace TechnoSewaMaui.ViewModel.Auth.Register
{
    public partial class PhoneNumberViewModel : BaseViewModel
    {
        private string _phoneNumber;
        public string PhoneNumber
        {
            get => _phoneNumber;
            set
            {
                _phoneNumber = value;
                OnPropertyChanged(nameof(PhoneNumber));
            }
        }

        public Command OnNextButtonClicked { get; }

        public Command OnSignInTapped { get; }

        public PhoneNumberViewModel()
        {
            OnNextButtonClicked = new Command(async () => await NextButtonClicked());
            OnSignInTapped = new Command(async () => await SignInTapped());
        }

        public async Task SignInTapped()
        {
            await Shell.Current.GoToAsync(nameof(SigninPage));
        }

        public async Task NextButtonClicked()
        {
            if (string.IsNullOrWhiteSpace(PhoneNumber))
            {
                await Shell.Current.DisplayAlert("Error", "No Number Entered", "Retry");
            }
            else if (PhoneNumber.Length != 10)
            {
                await Shell.Current.DisplayAlert("Error", "Invalid Phone Number", "Retry");
            }
            else
            {
                var request = new NotificationRequest
                {
                    NotificationId = 1,
                    Title = "OTP",
                    Subtitle = "Otp for TechnoSewa ",
                    Description = "Your otp is 1234 ",
                    BadgeNumber = 42,
                };
                LocalNotificationCenter.Current.Show(request);
                await Shell.Current.GoToAsync($"{nameof(OtpPage)}?phoneNumber={PhoneNumber}");
            }
        }
    }
}
