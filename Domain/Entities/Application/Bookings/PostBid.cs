using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.Base;
using Domain.Entities.User;
using Domain.Entities.User.PostDetails;

namespace Domain.Entities.Application.Bookings
{
    public class PostBid : DateAuditedEntity<int>
    {
        public string SolutionDescription { get; set; }
        public Double EstimationPrice { get; set; }
        public DateOnly ServiceDate { get; set; }

        public Post Post { get; set; }
        public Technician Technician { get; set; }
        public int Status { get; set; }
    }
}
