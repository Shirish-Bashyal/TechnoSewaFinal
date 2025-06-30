using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO.Chatbot;
using Microsoft.AspNetCore.Http;

namespace Application.Interfaces.LLM
{
    public interface ILLMFormatter
    {
        Task<string> FormatMessage(StringBuilder prompt);
        Task<string> TranscribeAudio(IFormFile audio);

        Task<string> InterpertImage(string jsonStructure);

        Task<string> IntentFinder(string query);
    }
}
