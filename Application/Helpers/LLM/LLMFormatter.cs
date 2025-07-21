using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Application.DTO.Chatbot;
using Application.Interfaces.LLM;
using Application.Response;
using GroqSharp;
using GroqSharp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
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

        public async Task<ChatbotBookingTaskDto> BookingTask(StringBuilder request)
        {
            var todayDate = DateTime.Now;
            var apiKey = _configuration["LLM:ApiKey"];
            var apiModel = _configuration["LLM:ApiModelChat"];
            var payload = new
            {
                messages = new[]
                {
                    //new
                    //{
                    //    role = "system",
                    //    content = "You are a helpful booking assistant.\r\nYour single goal is to gather the following six details from the user until each one is provided:\r\n\r\n1.you will collect Category,SubCategory,Address(lattitide and longitude),Time and Date.  Category can only be Plumbing. Category Example: Plumbing  \r\n2. SubCategory can only be  Washing Machine Installation and Toilet Unclogging for Plumbing category Example: Toilet Unclogging. \r\n3. ServiceDate (YYYY‑MM‑DD) or get today or tomorrow and conert to format \r\n\r\n5. Latitude\r\n6. Longitude\r\n7. TimeFrame (in hours) ex:  it can be any time between 6am to 18 pm\r\n Based on user query try to automatically figure out the above information \r\nGuidelines for the conversation\r\n• Ask for  missing items in the order listed above.\r\n• If the user supplies several items in one reply, quietly record all of  them as points  i.e Category : Plumbing, SubCategory: Toilet Clogging and so on and move on to the next missing item.\r\n. Ask the user address (if user havenot provided) and then convert it into latitude and longitude• If an answer is unclear or outside the expected format (e.g., a non‑numeric ID or an invalid date), politely re‑ask just for that item, giving an example of a valid response. No need to summarise the previous input in details\" +\n\"Your response should be in json format \" +\n\"{ Response: Your chat response, IsCompleted: true if all the parameters are gathered }   if all the info is gathered just stop asking for more and make is complete true, after making if complete true give json result only"
                    //},
                    new
                    {
                        role = "system",
                        content = $"You are a booking assistant. Your job is to extract the following six details from the user's message:\\n\\n1. **Category** – Only valid value: 'Plumbing'\\n2. **SubCategory** – Only valid values: 'Washing Machine Installation' or 'Toilet Unclogging'\\n3. **ServiceDate** – Accept terms like 'today', 'tomorrow' today date is ={todayDate},  or full date in 'YYYY-MM-DD' format and convert them accordingly.\\n4. **Latitude** – Extract directly if coordinates are provided. If only address is given (e.g., 'Butwal-14'), use the internal mapping to extract coordinates.\\n5. **Longitude** – Same as above.\\n6. **TimeFrame** – Accept times like '3 pm' if ranges like 'from 2 pm to 5 pm' pick 2 pm , and convert them to 24-hour format (e.g., '15').\\n\\n---\\n\\n### Address → Coordinate Mapping:\\n- 'Butwal-14' → Latitude: `27.6767`, Longitude: `83.4471`\\n- 'Bagbazar, Kathmandu' → Latitude: `27.7070`, Longitude: `85.3142`\\n- 'Pokhara-6' → Latitude: `28.2096`, Longitude: `83.9856`\\n\\n(Expand this as needed.)\\n\\n---\\n\\n### Rules:\\n- Return output in this **strict JSON format**:\\n```json\\n\\n  {{\\\"Response\\\": \\\"<assistant message>\\\",\\n  \\\"IsCompleted\\\": true/false,\\n  \\\"Category\\\": \\\"\\\",\\n  \\\"SubCategory\\\": \\\"\\\",\\n  \\\"ServiceDate\\\": \\\"\\\",\\n  \\\"Latitude\\\": \\\"\\\",\\n  \\\"Longitude\\\": \\\"\\\",\\n  \\\"TimeFrame\\\": \\\"\\\"\\n}}\\n```\\n- If all fields are extracted, set `IsCompleted` to `true`. Otherwise, ask only for the missing field.\\n- If the message includes multiple values, extract all without re-asking.\\n- If invalid/missing values are detected, politely re-ask with an example.\\n- Do not summarize previous inputs.\"\r\n"
                    },
                    new { role = "user", content = $"{request}" },
                    new
                    {
                        role = "assistant",
                        content = "Based on the provided question give meaningful instruction to user to solve their problem."
                    },
                    new { role = "user", content = "" }
                },
                model = apiModel,
                temperature = 0.2,
                max_completion_tokens = 256,
                top_p = 1,
                stream = false,
                response_format = new { type = "json_object" },
            };
            using var httpClient = new HttpClient();
            var json = JsonSerializer.Serialize(payload);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Bearer",
                apiKey
            );

            var response = await httpClient.PostAsync(
                "https://api.groq.com/openai/v1/chat/completions",
                content
            );

            var result = await response.Content.ReadAsStringAsync();
            JObject obj = JObject.Parse(result);
            string rawContent = (string)obj["choices"]![0]!["message"]!["content"]!;
            JObject inner = JObject.Parse(rawContent);
            string chatBotResponse = (string)inner["Response"]!;
            bool isCompleted = (bool)inner["IsCompleted"]!;

            if (isCompleted)
            {
                var bookingDetails = await ConvertResponseToJson(inner.ToString());

                return new ChatbotBookingTaskDto()
                {
                    ChatbotResponse = chatBotResponse,
                    IsCompleted = isCompleted,
                    BookingData = bookingDetails
                };
            }

            return new ChatbotBookingTaskDto()
            {
                ChatbotResponse = chatBotResponse,
                IsCompleted = isCompleted
            };
            //return rawContent;
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
                        "You are an intent classifier.\nClassify the user's input based on the following rules:\n\nIf the input is a general question asking for information, such as those containing \"what\", \"how\", \"why\", or other similar interrogatives, or if it is a general query seeking knowledge, return: (query)\n\nIf the input is a command or request asking to perform a task related to hiring or assigning a technician (e.g., \"replace a tap\", \"book me a technician\", \"unclog the bathroom\", etc.), return: (task)\n\nRespond with only one word : (task) and no explanation if the user input is task.\n\nIf the intent is (query) Respond with only one word :(query) with no any explanation.\n\nIf the user input is ambiguous just consider  it as a  query.\n\nExamples:\n\n“What is the best way to fix a leaking pipe?” → (query)\n\n“Book a plumber for me.” → (task)\n\n“How much does it cost to replace a tap?” → (query)\n\n“Send someone to fix my shower.” → (task)"
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

        public async Task<ChatbotJsonConverterResponse> ConvertResponseToJson(string request)
        {
            var apiKey = _configuration["LLM:ApiKey"];
            var apiModel = _configuration["LLM:ApiModelChat"];
            var payload = new
            {
                messages = new[]
                {
                    new
                    {
                        role = "system",
                        content = "you are a agent that converts my data into json as per the given model format by extracting them from my text model \n public string Category { get; set; }\n public string SubCategory { get; set; }\n public DateOnly ServiceDate { get; set; }\n  public Double Lattitude { get; set; }\n\n public Double Longitude { get; set; }\n\n public string TimeFrame { get; set; }"
                    },
                    new { role = "user", content = $"{request}" },
                    new
                    {
                        role = "assistant",
                        content = "Based on the provided question give meaningful instruction to user to solve their problem."
                    },
                    new { role = "user", content = "" }
                },
                model = apiModel,
                temperature = 0.1,
                max_completion_tokens = 256,
                top_p = 1,
                stream = false,
                response_format = new { type = "json_object" },
            };
            using var httpClient = new HttpClient();
            var json = JsonSerializer.Serialize(payload);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Bearer",
                apiKey
            );

            var response = await httpClient.PostAsync(
                "https://api.groq.com/openai/v1/chat/completions",
                content
            );

            var result = await response.Content.ReadAsStringAsync();
            JObject obj = JObject.Parse(result);
            string rawContent = (string)obj["choices"]![0]!["message"]!["content"]!;
            JObject inner = JObject.Parse(rawContent);

            var dto = JsonSerializer.Deserialize<ChatbotJsonConverterResponse>(
                rawContent,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            );
            dto.SubCategory = dto.SubCategory.Replace(" ", "");
            return dto;
        }
    }
}
