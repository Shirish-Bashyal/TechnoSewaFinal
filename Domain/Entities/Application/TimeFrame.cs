using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.Entity;

namespace Domain.Entities.Application
{
    public class TimeFrame : Entity<int>
    {
        public TimeOnly StartTime { get; set; }

        public TimeOnly EndTime { get; set; }
    }
}
