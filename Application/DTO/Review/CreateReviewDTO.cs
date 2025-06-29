using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Review
{
    public class CreateReviewDTO
    {
        public int BookingId { get; set; }
        public string? Comment { get; set; } = string.Empty;

        public double Rating { get; set; }

        public bool ByConsumer { get; set; }
    }
}
