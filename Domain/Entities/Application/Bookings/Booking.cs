using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.Entity;

namespace Domain.Entities.Application.Bookings
{
    public class Booking : Entity<int>
    {
        public int Status { get; set; }
        public SubCategoryBooking? SubCategoryBooking { get; set; }

        public PostBid? PostBid { get; set; }
    }
}
