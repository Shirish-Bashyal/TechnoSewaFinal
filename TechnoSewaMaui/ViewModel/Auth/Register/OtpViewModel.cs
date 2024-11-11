using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.LocalNotification;
using TechnoSewaMaui.ViewModel.Base;
using TechnoSewaMaui.Views.Auth.Register;

namespace TechnoSewaMaui.ViewModel.Auth.Register
{
    [QueryProperty(nameof(PhoneNumber), "phoneNumber")]
    public partial class OtpViewModel : BaseViewModel
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
        private string _otp;

        public string Otp
        {
            get => _otp;
            set
            {
                _otp = value;
                OnPropertyChanged(nameof(Otp));
            }
        }

        public Command OnNextButtonClicked { get; }

        public Command OnResendButtonClicked { get; }

        public OtpViewModel()
        {
            OnNextButtonClicked = new Command(async () => await NextButtonClicked());
            OnResendButtonClicked = new Command(async () => await ResendButtonClicked());
        }

        public async Task NextButtonClicked()
        {
            if ((string.IsNullOrWhiteSpace(PhoneNumber)) || (string.IsNullOrWhiteSpace(Otp)))
            {
                await Shell.Current.DisplayAlert("Error", "Please Enter Otp", "ok");
            }
            else
            {
                if (Otp == "1234")
                {
                    await Shell.Current.GoToAsync(
                        $"{nameof(RegisterPage)}?phoneNumber={PhoneNumber}"
                    );
                }
                else
                {
                    await Shell.Current.DisplayAlert("Error", "Invalid Otp", "Retry");
                }
            }
        }

        public async Task ResendButtonClicked()
        {
            var request = new NotificationRequest
            {
                NotificationId = 1,
                Title = "OTP",
                Subtitle = "Otp for TechnoSewa ",
                Description = "Your otp is 1234 ",
                BadgeNumber = 42,
            };
            await LocalNotificationCenter.Current.Show(request);
            await Shell.Current.DisplayAlert("Success", "Otp is sent", "ok");
        }
    }
}
