using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Booking.Technician
{
    public class GetTechnicianBookingsDTO
    {
        public List<PendindBookingsDTO>? PendingBookings { get; set; }

        public List<ActiveBookingsDTO>? ActiveBookings { get; set; }

        public List<CompletedBookingsDTO>? CompletedBookings { get; set; }
    }
}
