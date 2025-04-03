using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.Application;
using Domain.Entities.Base;

namespace Domain.Entities.User.PostDetails
{
    public class Post : DateAuditedEntity<int>
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public Category Category { get; set; }

        public ApplicationUser User { get; set; }

        public ICollection<PhotoPath> Photos { get; set; }

        public Double Lattitude { get; set; }

        public Double Longitude { get; set; }

        public int Status { get; set; }
    }
}
