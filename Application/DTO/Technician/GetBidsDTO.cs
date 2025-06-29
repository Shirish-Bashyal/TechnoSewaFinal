using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Technician
{
    public class GetBidsDTO
    {
        public string SolutionDescription { get; set; }
        public Double EstimationPrice { get; set; }
        public DateOnly ServiceDate { get; set; }

        public int PostId { get; set; }

        public string PostTitle { get; set; }

        public int BidId { get; set; }
    }
}
