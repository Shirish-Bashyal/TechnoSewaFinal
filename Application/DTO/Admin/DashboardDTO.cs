using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Admin
{
    public class DashboardDTO
    {
        public int TotalUsers { get; set; }
        public int TotalConsumers { get; set; }
        public int TotalTechnicians { get; set; }

        public int TotalActiveUsers { get; set; }
        public int TotalActiveTechnicians { get; set; }
        public int TotalActiveConsumers { get; set; }

        public int TotalBookings { get; set; }
        public int TotalPendingBookings { get; set; }
        public int TotalActiveBookings { get; set; }
        public int TotalCompletedBookings { get; set; }

        public double TotalRevenue { get; set; }
    }
}
