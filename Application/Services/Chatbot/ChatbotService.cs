using Application.DTO.Chatbot;
using Application.Helpers.LLM;
using Application.Interfaces.Chatbot;
using Application.Interfaces.LLM;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

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

        public async Task<string> SendImage(ImageDto request)
        {
            var isValidSize = await CheckImageValidity(request.Image);
            if (isValidSize)
            {
                var imageBase64 = await ConvertToBase64UrlAsync(request.Image);
                if (imageBase64 != null)
                {
                    var jsonStructure = $@"[
                {{
                    ""type"": ""text"",
                    ""text"": ""{request.Text}""
                }},
                {{
                    ""type"": ""image_url"",
                    ""image_url"": {{
                        ""url"": ""{imageBase64}""
                    }}
                }}]";

                    var response = await _llmFormatter.InterpertImage(jsonStructure);
                    if (response != null)
                    { 
                        return response;
                    }
                    else
                    {
                        return "Oops! Something went wrong";
                    }
                }
                else
                {

                    return "Image Url size should not exceed 4MB.";
                }
            }
            else
            {

                return "Image size should not exceed 20MB.";
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


        public async Task<string> ConvertToBase64UrlAsync(IFormFile imageFile)
        {
            if (imageFile == null || imageFile.Length == 0)
                return null;

            using (var memoryStream = new MemoryStream())
            {
                await imageFile.CopyToAsync(memoryStream);
                var imageBytes = memoryStream.ToArray();
                var sizeInMB = imageBytes.Length / (1024 * 1024);
                if (sizeInMB <= 4)
                {
                    var base64String = Convert.ToBase64String(imageBytes);

                    // Determine the content type (e.g., image/png or image/jpeg)
                    var contentType = imageFile.ContentType;
                    return $"data:{contentType};base64,{base64String}";
                }
                else
                {
                    return null;
                }
            }

        }

            public async Task<bool> CheckImageValidity(IFormFile imageFile)
            {
                var stream = imageFile.OpenReadStream();


                if (imageFile.Length > 1024 * 1024 * 20)
                {
                    return false;

                }
                else
                {
                    return true;
                }

            }

       
    }
}
