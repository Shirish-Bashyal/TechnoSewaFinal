using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnoSewaMaui.Model
{
   public class SendMessageWithImageModel
    {
        public string Question { get; set; }
        public byte[] ImageFile { get; set; }
        public string ImageName { get; set; }
    }
}
