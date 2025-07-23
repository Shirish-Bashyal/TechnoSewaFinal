using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Response;

namespace Application.DTO.Chatbot
{
    public class ChatbotBookingTaskDto
    {
        public required string ChatbotResponse { get; set; }
        public bool IsCompleted { get; set; }

        public ChatbotJsonConverterResponse? BookingData { get; set; }
    }
}
