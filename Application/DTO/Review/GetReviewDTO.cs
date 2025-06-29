using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Review
{
    public class GetReviewDTO
    {
        public double AverageRating { get; set; }

        public List<string> Reviews { get; set; }
    }
}
