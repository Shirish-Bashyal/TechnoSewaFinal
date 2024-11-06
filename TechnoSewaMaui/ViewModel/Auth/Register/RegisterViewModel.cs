using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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
            }
        }

        [ObservableProperty]
        private string _fullName;

        [ObservableProperty]
        private string _email;

        [ObservableProperty]
        private string _password;

        [ObservableProperty]
        private string _confirmPassword;

        [ObservableProperty]
        private string _city;

        [ObservableProperty]
        private string _wardNo;

        [ObservableProperty]
        private string _toleName;

        public RegisterViewModel() { }

        [RelayCommand]
        public async Task RegisterButtonCLicked()
        {
            await Shell.Current.DisplayAlert("Success", "It is clicked", "OK");
        }
    }
}
