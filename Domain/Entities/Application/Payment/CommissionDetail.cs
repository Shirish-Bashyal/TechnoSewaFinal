using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.Entity;
using Domain.Entities.User;

namespace Domain.Entities.Application.Payment
{
    public class CommissionDetail : Entity<int>
    {
        [ForeignKey(nameof(TechnicianId))]
        public Technician Technician { get; set; }

        public int TechnicianId { get; set; }

        public Double CommissionAmount { get; set; }

        public bool IsLimitReached { get; set; }
    }
}
