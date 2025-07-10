using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.Application.Bookings;
using Domain.Entities.User;

namespace Application.DTO.Technician
{
    public class GetTechnicianAllDetails
    {
        public int TechnicianId { get; set; }

        public bool IsVerified { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string PhoneNumber { get; set; }
        public string SecondPhoneNumber { get; set; }
    }
}
