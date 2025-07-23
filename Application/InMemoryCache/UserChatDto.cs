using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.InMemoryCache
{
   public class UserChatDto
    {
        public string Intent { get; set; }
        public string UserQuery { get; set; }
        public string ChatbotResponse { get; set; }
    }
}
