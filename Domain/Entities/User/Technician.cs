using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.Application.Bookings;
using Domain.Entities.Entity;
using Domain.Interfaces.Entity;

namespace Domain.Entities.User
{
    public class Technician : DateAuditedEntity<int>
    {
        public string UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; }

        public string SecondPhoneNumber { get; set; }

        public Double Latitude { get; set; }
        public Double Longitude { get; set; }

        public ICollection<SubCategoryBooking>? TechnicianBookings { get; set; }
        public ICollection<PostBid>? PostBids { get; set; }
    }
}
