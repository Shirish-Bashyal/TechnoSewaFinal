using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO.Booking.Consumer;
using Domain.Entities.Application;

namespace Application.DTO.Booking
{
    public class GetConsumerBookingsDTO
    {
        public List<PendingBookingsDTO>? PendingBookings { get; set; }

        public List<ActiveBookingsDTO>? ActiveBookings { get; set; }

        public List<CompletedBookingsDTO>? CompletedBookings { get; set; }
    }
}
