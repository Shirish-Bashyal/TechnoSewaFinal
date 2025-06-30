using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Application.DTO.Chatbot;
using Application.Interfaces.LLM;
using GroqSharp;
using GroqSharp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Application.Helpers.LLM
{
    public class LLMFormatter : ILLMFormatter
    {
        public readonly IConfiguration _configuration;
        private readonly ITextTokenizer _textTokenizer;

        public LLMFormatter(IConfiguration configuration, ITextTokenizer textTokenizer)
        {
            _configuration = configuration;
            _textTokenizer = textTokenizer;
        }

        public async Task<string> FormatMessage(StringBuilder prompt)
        {
            var apiKey = _configuration["LLM:ApiKey"];
            var apiModel = _configuration["LLM:ApiModelChat"];

            IGroqClient groqClient = new GroqClient(apiKey, apiModel)
                .SetTemperature(0.2) // randomness of response   0 = more deterministic , 1= random
                .SetMaxTokens(256) // limits the output length
                .SetTopP(1) //
                .SetStop("NONE"); // tells the model when to stop
            var response = await groqClient.CreateChatCompletionAsync(
                new Message
                {
                    Role = MessageRoleType.System,
                    Content =
                        "You are a helpful assistant designed to help customers to solve their problem and answer to any of their queries related to any technical issues in their daily life like broken pipes or gas stove and others.If the question has matched strings and database response give solution based on the database response combined with the user question. If the question doesn't have any matched strings and database response give answer based on your understanding. The answers should be short but meaningful.If the question is not related to electrical , plumbing or any mechanical issue please don't give any solution , give some answer like sorry i'm not trained for that but only for solving electrial and mechanical problems  and give response in a single sentence but not a whole paragraph in such case."
                },
                new Message
                {
                    Role = MessageRoleType.Assistant,
                    Content =
                        "Based on the provided question give meaningful instruction to user to solve their problem."
                },
                new Message { Role = MessageRoleType.User, Content = prompt.ToString() }
            );

            return response;
        }

        public async Task<string> IntentFinder(string query)
        {
            var apiKey = _configuration["LLM:ApiKey"];
            var apiModel = _configuration["LLM:ApiModelChat"];

            IGroqClient groqClient = new GroqClient(apiKey, apiModel)
                .SetTemperature(0) // randomness of response   0 = more deterministic , 1= random
                .SetMaxTokens(50) // limits the output length
                .SetTopP(1) //
                .SetStop("NONE"); // tells the model when to stop
            var response = await groqClient.CreateChatCompletionAsync(
                new Message
                {
                    Role = MessageRoleType.System,
                    Content =
                        "You are a system to determine the intent of the question. Question will be of 2 types:- 1)simple question asking for result, if this is the intent return (query). 2)user asking to perform a task related to hiring a technician by giving a task as-: replace a tap, unclog the bathroom. then return intent as (task) "
                },
                new Message
                {
                    Role = MessageRoleType.Assistant,
                    Content =
                        "Based on the provided question determine the intent of the question and return either query or task ."
                },
                new Message { Role = MessageRoleType.User, Content = query.ToString() }
            );

            return response;
        }

        public async Task<string> InterpertImage(string jsonStructure)
        {
            var apiKey = _configuration["LLM:ApiKey"];
            var apiModel = _configuration["LLM:ApiModelVision"];
            IGroqClient groqClient = new GroqClient(apiKey, apiModel)
                .SetTemperature(0.2) // randomness of response   0 = more deterministic , 1= random
                .SetMaxTokens(256)
                .SetTopP(1)
                .SetStop("NONE");

            var response = await groqClient.CreateChatCompletionAsync(
                new Message
                {
                    Role = MessageRoleType.System,
                    Content =
                        "You are a helpful assistant designed to help customers to solve their problem and answer to any of their queries related to any technical issues in their daily life like broken pipes or gas stove and others.If the question has matched strings and database response give solution based on the database response combined with the user question. If the question doesn't have any matched strings and database response give answer based on your understanding. The answers should be short but meaningful.If the question is not related to electrical , plumbing or any mechanical issue please don't give any solution , give some answer like sorry i'm not trained for that but only for solving electrial and mechanical problems  and give response in a single sentence but not a whole paragraph in such case.Also accept if the image is given and check what the image is about and help to analyese and solve if the image has above problems "
                },
                new Message
                {
                    Role = MessageRoleType.Assistant,
                    Content =
                        "Based on the provided question give meaningful instruction to user to solve their problem."
                },
                new Message { Role = MessageRoleType.User, Content = $"{jsonStructure}" }
            );

            return response;
        }

        public async Task<string> TranscribeAudio(IFormFile audio)
        {
            var apiKey = _configuration["LLM:ApiKey"];
            var apiModel = _configuration["LLM:ApiModelAudio"];

            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Bearer",
                apiKey
            );

            using var content = new MultipartFormDataContent();

            var streamContent = new StreamContent(audio.OpenReadStream());
            content.Add(streamContent, "file", audio.FileName);
            streamContent.Headers.ContentType = new MediaTypeHeaderValue("audio/m4a");
            content.Add(new StringContent(apiModel), "model");

            var response = await client.PostAsync(
                "https://api.groq.com/openai/v1/audio/transcriptions",
                content
            );

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"Groq transcription failed: {error}");
            }

            var json = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(json);
            var transcription = doc.RootElement.GetProperty("text").GetString();

            return transcription;
        }
    }
}
