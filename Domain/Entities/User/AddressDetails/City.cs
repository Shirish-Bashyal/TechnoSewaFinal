using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.Base;

namespace Domain.Entities.User.AddressDetails
{
    public class City : Entity<int>
    {
        public string Name { get; set; }
    }
}
