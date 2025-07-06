using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.Entity;

namespace Domain.Entities.Application
{
    public class SubCategory : Entity<int>
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public Double Price { get; set; }

        public Category Category { get; set; }
    }
}
