using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.User.Consumer
{
    public class GetBidDTO
    {
        public string SolutionDescription { get; set; }
        public Double EstimationPrice { get; set; }
        public DateOnly ServiceDate { get; set; }

        public string? TechnicianName { get; set; }

        public int BidId { get; set; }

        public int? TechnicianId { get; set; }
    }
}
