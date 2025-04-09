using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnoSewaMaui.Model
{
    public class ProfileModel
    {
        public string Name { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public int WardNo { get; set; }
        public string ToleName { get; set; } = string.Empty;
    }
}
