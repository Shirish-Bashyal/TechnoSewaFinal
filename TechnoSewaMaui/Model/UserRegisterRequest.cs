using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace TechnoSewaMaui.Model
{
    public partial class UserRegisterRequest : ObservableObject
    {
        [ObservableProperty]
        private string phoneNumber;

        [ObservableProperty]
        private string email;

        [ObservableProperty]
        private string password;

        [ObservableProperty]
        private string confirmPassword;

        [ObservableProperty]
        private string fullName;

        [ObservableProperty]
        private string city;

        [ObservableProperty]
        private int wardNo;

        [ObservableProperty]
        private string toleName;
    }
}
