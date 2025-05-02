using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.User.PostDetails;

namespace Application.DTO.Technician
{
    public class CreateBidDTO
    {
        public int PostId { get; set; }
        public string SolutionDescription { get; set; }
        public Double EstimationPrice { get; set; }
        public DateOnly ServiceDate { get; set; }
    }
}
