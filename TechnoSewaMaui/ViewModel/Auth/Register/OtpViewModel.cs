using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnoSewaMaui.ViewModel.Base;

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

        public OtpViewModel()
        {
            OnNextButtonClicked = new Command(async () => await NextButtonClicked());
        }

        public async Task NextButtonClicked()
        {
            //Shell.Current.GoToAsync(nameof(OtpPage));

            await Shell.Current.DisplayAlert("clicked", "yes its clicked", "ok");

            //await Shell.Current.GoToAsync($"{nameof(RegisterPage)}?phoneNumber={PhoneNumber}");
        }
    }
}
