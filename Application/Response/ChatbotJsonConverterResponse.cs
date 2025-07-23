using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Response
{
    public class ChatbotJsonConverterResponse
    {
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public DateOnly ServiceDate { get; set; }

        public Double Lattitude { get; set; }

        public Double Longitude { get; set; }

        public string TimeFrame { get; set; }
    }
}
