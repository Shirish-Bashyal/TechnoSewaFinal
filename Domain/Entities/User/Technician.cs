using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.Application.Bookings;
using Domain.Entities.Base;
using Domain.Interfaces.Entity;

namespace Domain.Entities.User
{
    public class Technician : DateAuditedEntity<int>
    {
        public ApplicationUser User { get; set; }

        public string SecondPhoneNumber { get; set; }

        public ICollection<SubCategoryBooking>? TechnicianBookings { get; set; }
        public ICollection<PostBid>? PostBids { get; set; }
    }
}
