using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Application.DTO.Review;

namespace Application.DTO.Technician
{
    public class GetTechnicianDetailsDTO
    {
        public int TechnicianId { get; set; }
        public string Name { get; set; }

        // public string PhoneNumber { get; set; }

        public Double Distance { get; set; }

        public GetReviewDTO? Reviews { get; set; }

        //  public double Distance { get; set; }
        //avg rating
        //list of reviews
    }
}
