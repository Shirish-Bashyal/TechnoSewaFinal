using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO.User.Post;
using Domain.Entities.Application;

namespace Application.DTO.Booking.Consumer
{
    public class CompletedBookingsDTO
    {
        public int BookingId { get; set; }

        public string Title { get; set; }

        public Double Price { get; set; }

        public DateOnly ServiceDate { get; set; }

        public TimeFrame TimeFrame { get; set; }

        public string TechnicianName { get; set; }

        public double Lattitude { get; set; }

        public double Longitude { get; set; }
    }
}
