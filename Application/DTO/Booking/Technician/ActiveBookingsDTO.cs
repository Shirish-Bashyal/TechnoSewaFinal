using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.Application;

namespace Application.DTO.Booking.Technician
{
    public class ActiveBookingsDTO
    {
        public int BookingId { get; set; }

        public string Title { get; set; }

        public Double Price { get; set; }

        public DateOnly ServiceDate { get; set; }

        public TimeFrame? TimeFrame { get; set; }

        public string ConsumerName { get; set; }

        public string ConsumerPhoneNumber { get; set; }

        public double Lattitude { get; set; }

        public double Longitude { get; set; }
    }
}
