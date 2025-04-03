using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.User.Consumer
{
    public class ConsumerProfileDTO
    {
        public string Name { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string Role { get; set; }

        // public int TotalBookings { get; set; }

        // public int ActiveBookings { get; set; }

        public string City { get; set; }

        public int WardNo { get; set; }

        public string ToleName { get; set; }

        // public int NoOfReviews {  get; set; } }
    }
}
