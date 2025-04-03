using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnoSewaMaui.ViewModel.Base;

namespace TechnoSewaMaui.ViewModel.Profile
{
    public partial class ProfileViewModel : BaseViewModel
    {
        public string UserName { get; set; } = "John Doe";
        public string PhoneNumber { get; set; } = "+977 98XXXXXXXX";
        public string Email { get; set; } = "john@example.com";
    }
}
