using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.Entity;

namespace Domain.Entities.Chatbot
{
    public class QuestionResponse : Entity<string>
    {
        public string Keyword { get; set; }
        public string Response { get; set; }
    }
}
