using Application.DTO.Chatbot;
using Application.Helpers.LLM;
using Application.Interfaces.Chatbot;
using Application.Interfaces.LLM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Chatbot
{
    public class ChatbotService : IChatbotService
    {
        private readonly ILLMFormatter _llmFormatter;
        private readonly ITextTokenizer _textTokenizer;

        public ChatbotService(ILLMFormatter llmFormatter, ITextTokenizer textTokenizer)
        {
            _llmFormatter = llmFormatter;
            _textTokenizer = textTokenizer;
        }

        public async Task<string> SendAudio(AudioDto request)
        {
            var question = await _llmFormatter.TranscribeAudio(request.Audio);
            if (question != null)
            {
                var requestModel = new MessageDto()
                { Question = question };
                var response = await SendMessage(requestModel);
                if (response != null)
                {
                    return response;
                }
                else
                {
                    return string.Empty;
                }
            }
            else
            {
                return string.Empty;
            }
        }

        public async Task<string> SendMessage(MessageDto request)
        {
            var dbResponse = await _textTokenizer.GetFinalResponse(request);  
            var matchedKeywords = await _textTokenizer.GetMatchedStrings(request);
            var prompt = new StringBuilder();
            var question = request.Question;
            prompt.AppendLine($"User's Question: {question}");
            prompt.AppendLine($"Matched Strings: {matchedKeywords}");
            prompt.AppendLine($"Database Response: {dbResponse}");
           var response = await _llmFormatter.FormatMessage(prompt);
            if (response != null)
            {
                return response;
            }
            else {
                return "Oops! Something went wrong";
            }
        }
    }
}
