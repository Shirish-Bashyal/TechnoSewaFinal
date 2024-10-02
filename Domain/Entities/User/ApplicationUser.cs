using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.Base;
using Domain.Entities.User.AddressDetails;
using Domain.Interfaces.Entity;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.User
{
    public class ApplicationUser : IdentityUser, IDateAudited //extending the identitymodel for additional properties
    {
        public DateTime? AddedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public Address Address { get; set; }

        //store a profile picture of all user as nullable
    }
}
