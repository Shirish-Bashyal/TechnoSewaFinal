using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.Entity;
using Domain.Entities.User;
using Domain.Interfaces.Entity;

namespace Domain.Entities.Application.Payment
{
    public class TransactionDetail : EntityWithCreationDate<int>
    {
        public string Pid { get; set; }

        public int TechnicianId { get; set; }

        [ForeignKey(nameof(TechnicianId))]
        public Technician Technician { get; set; }

        public Double Amount { get; set; }
    }
}
