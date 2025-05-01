using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnoSewaMaui.Model
{
    public class ChatMessageModel
    {
        public string Question { get; set;}
        public ImageSource ImageSource { get; set;}
        public bool IsSentByUser { get; set;}
    }
}
