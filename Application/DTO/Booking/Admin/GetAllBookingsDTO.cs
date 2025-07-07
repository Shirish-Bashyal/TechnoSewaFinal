using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.Application;

namespace Application.DTO.Booking.Admin
{
    public class GetAllBookingsDTO
    {
        public int Id { get; set; }

        public string Status { get; set; }

        public string Title { get; set; }

        public Double? Price { get; set; }

        public DateOnly? ServiceDate { get; set; }

        public string ConsumerName { get; set; }

        public string ConsumerPhone { get; set; }

        public string? TechnicianName { get; set; }
        public string? TechnicianPhone { get; set; }
    }
}
