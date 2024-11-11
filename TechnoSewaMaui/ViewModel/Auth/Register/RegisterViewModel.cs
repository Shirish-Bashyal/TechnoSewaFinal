using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TechnoSewaMaui.Model;
using TechnoSewaMaui.Services.Auth.Register;
using TechnoSewaMaui.ViewModel.Base;

namespace TechnoSewaMaui.ViewModel.Auth.Register
{
    [QueryProperty(nameof(PhoneNumber), "phoneNumber")]
    public partial class RegisterViewModel : BaseViewModel
    {
        private string _phoneNumber;

        public string PhoneNumber
        {
            get => _phoneNumber;
            set
            {
                _phoneNumber = value;
                OnPropertyChanged(nameof(PhoneNumber));

                // Manually assign the PhoneNumber to the RegistrationData object
                RegistrationData.PhoneNumber = value;
            }
        }
        public UserRegisterRequest RegistrationData { get; set; }

        private readonly RegisterServices _registerService;

        public RegisterViewModel(RegisterServices registerService)
        {
            RegistrationData = new UserRegisterRequest();
            _registerService = registerService;
        }

        [RelayCommand]
        public async Task RegisterButtonCLicked()
        {
            var validity = AreRegistrationDataFieldsValid(RegistrationData);
            if (validity)
            {
                if (RegistrationData.PhoneNumber.Length != 10)
                {
                    await Shell.Current.DisplayAlert("Error", "Invalid Phone Number", "Retry");
                }
                else if (RegistrationData.Password.Length < 4)
                {
                    await Shell.Current.DisplayAlert(
                        "Error",
                        "Password must have 5 characters with a capital,symbol and number",
                        "Retry"
                    );
                }
                else if (RegistrationData.Password != RegistrationData.ConfirmPassword)
                {
                    await Shell.Current.DisplayAlert(
                        "Error",
                        "Password and Confirm Password mismatch",
                        "Retry"
                    );
                }
                else if (RegistrationData.WardNo < 1)
                {
                    await Shell.Current.DisplayAlert("Error", "Invalid WardNumber", "Retry");
                }
                else
                {
                    var result = await _registerService.RegisterUser(RegistrationData);
                    if (result)
                    {
                        await Shell.Current.DisplayAlert("Success", "Data registered", "Ok");
                    }
                    else
                    {
                        await Shell.Current.DisplayAlert("Error", "failed", "Retry");
                    }
                }
            }
            else
            {
                await Shell.Current.DisplayAlert("Error", "Invalid Data", "Retry");
            }
        }

        public static bool AreRegistrationDataFieldsValid(UserRegisterRequest RegistrationData)
        {
            var properties = typeof(UserRegisterRequest).GetProperties();

            foreach (var property in properties)
            {
                if (property.GetValue(RegistrationData) is string stringValue)
                {
                    if (string.IsNullOrWhiteSpace(stringValue))
                    {
                        return false;
                    }
                }
                else if (property.GetValue(RegistrationData) == null)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
