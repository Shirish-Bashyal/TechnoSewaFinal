using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO.Chatbot;

namespace Application.Interfaces.Chatbot
{
    public interface IChatbotService
    {
        Task<string> SendMessage(MessageDto request);
        Task<string> SendAudio(AudioDto request);
        Task<string> SendImage(ImageDto request);

        Task<string> FindIntent(string userQuery);
    }
}
