using Application.DTO.Chatbot;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.LLM
{
    public interface ILLMFormatter
    {
        Task<string> FormatMessage(MessageDto request);

        Task<string> FormatAudio(AudioDto request);
        Task<string> TranscribeAudio(IFormFile audio);
    }
}
