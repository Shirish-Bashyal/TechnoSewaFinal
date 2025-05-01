using Application.DTO.Chatbot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Chatbot
{
    public interface IChatbotService
    {
        Task<string> SendMessage(MessageDto request);
        Task<string> SendAudio(AudioDto request);
    }
}
