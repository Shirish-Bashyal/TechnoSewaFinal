using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CommunityToolkit.Maui.Alerts;
using IntelliJ.Lang.Annotations;
using Plugin.Maui.Biometric;
using TechnoSewaMaui.ViewModel.Base;
using TechnoSewaMaui.Views.Auth.Register;

namespace TechnoSewaMaui.ViewModel.Auth.SignIn
{
    public partial class SignInViewModel : BaseViewModel
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

        private string _password;
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        //private readonly UserServices _userServices;

        public Command OnSignInTapped { get; }
        public Command OnRegisterTapped { get; }
        public Command OnFingerPrintTapped { get; }

        public SignInViewModel()
        {
            // _userServices = userServices;
            OnSignInTapped = new Command(async () => await SignInTapped());
            OnRegisterTapped = new Command(async () => await RegisterTapped());
            OnFingerPrintTapped = new Command(async () => await FingerPrintTapped());
        }

        public async Task FingerPrintTapped()
        {
            var result = await BiometricAuthenticationService.Default.AuthenticateAsync(
                new AuthenticationRequest()
                {
                    Title = "Please enter your fingerprint",
                    NegativeText = "Cancel Authentication",
                },
                CancellationToken.None
            );

            //var status = BiometricHwStatus.LockedOut;
            if (result.Status == BiometricResponseStatus.Success)
            {
                await Shell.Current.DisplayAlert(
                    "Success",
                    "Fingerprint authenticated successfully",
                    "Ok!"
                );
                //await Shell.Current.GoToAsync("//HomePage");
                // await Navigation.PushAsync(new HomePage());
                //Microsoft.Maui.Controls.Application.Current.MainPage = new HomePage();
            }
            else
            {
                var errorMsg = result.ErrorMsg;
                var remove = "code:";
                string pattern = $@"{remove}\s*\d{{1,2}}\s*(.*)";
                Match match = Regex.Match(errorMsg, pattern);
                string resultError = match.Groups[1].Value.Trim();
                // string resultError = Regex.Replace(errorMsg, pattern, "").Trim();
                // await Shell.Current.DisplayAlert( $"{resultError}","", "Ok!"); }
                var toast = Toast.Make(
                    $"{resultError}",
                    CommunityToolkit.Maui.Core.ToastDuration.Long,
                    14
                );
                await toast.Show();
            }

            //var errorMsg = result.ErrorMsg;
        }

        public async Task RegisterTapped()
        {
            await Shell.Current.GoToAsync(nameof(PhoneNumberPage));
        }

        public async Task SignInTapped()
        {
            IsBusy = true;
            if (!string.IsNullOrWhiteSpace(PhoneNumber) && !string.IsNullOrWhiteSpace(Password))
            {
                await Shell.Current.DisplayAlert("Success", "User login successful", "Ok!");
                //var requestModel = new UserLoginRequest { PhoneNumber = PhoneNumber, Password = Password, };

                //var request = await _userServices.LoginUser(requestModel);
                //if (request == true)
                //{
                //    await Shell.Current.DisplayAlert("Success", "User login successful", "Ok!");
                //}
                //else
                //{
                //    await Shell.Current.DisplayAlert("Error", "Invalid Credential!!", "Ok!");
                //}
            }
        }
    }
}
