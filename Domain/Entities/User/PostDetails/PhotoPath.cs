using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.Entity;

namespace Domain.Entities.User.PostDetails
{
    public class PhotoPath : Entity<int>
    {
        public string Path { get; set; }

        public Post Post { get; set; }
    }
}
