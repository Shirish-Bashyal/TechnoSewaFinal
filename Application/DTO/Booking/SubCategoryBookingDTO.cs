using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Booking
{
    public class SubCategoryBookingDTO
    {
        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }
        public DateOnly ServiceDate { get; set; }
        public int TechnicianID { get; set; }
        public Double Lattitude { get; set; }

        public Double Longitude { get; set; }

        public int TimeFrame { get; set; }
    }
}
