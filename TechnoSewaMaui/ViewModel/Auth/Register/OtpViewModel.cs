using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                await Shell.Current.GoToAsync($"{nameof(RegisterPage)}?phoneNumber={PhoneNumber}");
            }
        }

        public async Task ResendButtonClicked()
        {
            //send a local notification
            await Shell.Current.DisplayAlert("clicked", "yes its clicked", "ok");
        }
    }
}
