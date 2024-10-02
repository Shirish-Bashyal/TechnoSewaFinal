using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.Base;

namespace Domain.Entities.User.AddressDetails
{
    public class Address : Entity<int>
    {
        public City City { get; set; }

        public int WardNo { get; set; }

        public string ToleName { get; set; }
    }
}
