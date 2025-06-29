using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Technician
{
    public class GetByFilterDTO
    {
        public DateOnly Date { get; set; }

        public int TimeFrameEnum { get; set; }

        public Double Latitude { get; set; }

        public Double Longitude { get; set; }
    }
}
