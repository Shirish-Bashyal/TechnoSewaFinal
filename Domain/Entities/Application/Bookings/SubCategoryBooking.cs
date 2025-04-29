using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.Base;
using Domain.Entities.User;

namespace Domain.Entities.Application.Bookings
{
    public class SubCategoryBooking : DateAuditedEntity<int>
    {
        public Category Category { get; set; }

        public SubCategory SubCategory { get; set; }

        public Double Lattitude { get; set; }

        public Double Longitude { get; set; }

        public DateOnly ServiceDate { get; set; }

        public TimeFrame TimeFrame { get; set; }

        public string ConsumerId { get; set; }
        public ApplicationUser Consumer { get; set; }

        // Foreign Key for Technician
        public int TechnicianId { get; set; }
        public Technician Technician { get; set; }

        public int BookingId { get; set; }
        public Booking Booking { get; set; }
    }
}
