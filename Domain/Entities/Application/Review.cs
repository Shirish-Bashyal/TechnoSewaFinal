using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.Application.Bookings;
using Domain.Entities.Entity;

namespace Domain.Entities.Application
{
    public class Review : DateAuditedEntity<int>
    {
        public Booking Booking { get; set; }
        public string? Comment { get; set; }

        public double Rating { get; set; }

        public bool ByConsumer { get; set; }
    }
}
