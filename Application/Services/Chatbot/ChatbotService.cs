using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO.Booking;
using Application.DTO.Chatbot;
using Application.Helpers.LLM;
using Application.InMemoryCache;
using Application.Interfaces.Chatbot;
using Application.Interfaces.LLM;
using Application.Response;
using GroqSharp.Models;
using Microsoft.AspNetCore.Http;
using static System.Net.Mime.MediaTypeNames;
using static Application.Constants.Enums.CategoryEnums;
using static Application.Constants.Enums.SubCategoryEnums;
using static Application.Constants.Enums.TimeFrameEnums;

namespace Application.Services.Chatbot
{
    public class ChatbotService : IChatbotService
    {
        private readonly ILLMFormatter _llmFormatter;
        private readonly ITextTokenizer _textTokenizer;
        private readonly UserChatDb _userChatDb;

        public ChatbotService(ILLMFormatter llmFormatter, ITextTokenizer textTokenizer,UserChatDb userChatDb)
        {
            _llmFormatter = llmFormatter;
            _textTokenizer = textTokenizer;
            _userChatDb = userChatDb;
        }

        public async Task<string> SendAudio(AudioDto request)
        {
            var question = await _llmFormatter.TranscribeAudio(request.Audio);
            if (question != null)
            {
                var requestModel = new MessageDto() { Question = question };
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
                    var jsonStructure =
                        $@"[
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
            else
            {
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

        public async Task<string> FindIntent(string customerId, string userQuery)
        {
            var result = await _llmFormatter.IntentFinder(userQuery);
            var previousChat = _userChatDb.GetChat(customerId);
            if (previousChat == null)
            {
                var userChat = new UserChatDto()
                {
                    Intent = result,
                    UserQuery = userQuery,
                    //ChatbotResponse = previousChat.ChatbotResponse,
                };
                _userChatDb.AddChat(customerId, userChat);
            }
            return result;
        }
        public async Task<ServiceResponse<string>> MainChat(string customerId,string userQuery)
        {
            var previousChat = _userChatDb.GetChat(customerId);
            if (previousChat == null)
            {
                await FindIntent(customerId, userQuery);
            }
            //var result = await FindIntent(customerId,userQuery);
           var userChat = _userChatDb.GetChat(customerId);
            if (userChat.Intent == "task")
                    {
                    var response = await BookingTask(customerId, userQuery);
                    if (response != null)
                    {
                        return response;
                    }
                }
                    else if (userChat.Intent == "query")
                {
                    //SendMessage()
                    return new ServiceResponse<string>()
                    {
                    };
                }
                else
                {
                    return new ServiceResponse<string>()
                    {
                        Success = false,
                        //Data = bookingResponse.ChatbotResponse
                        Message = "Please describe a your problem a bit more so that a chatbot can understand and try to solve it",
                    };
                }
            return new ServiceResponse<string>()
            {
                Success = false,
                //Data = bookingResponse.ChatbotResponse
                Message = "Please describe a your problem a bit more so that a chatbot can understand and try to solve it",
            };


        }

        public async Task<ServiceResponse<string>> BookingTask(string customerId,string userQuery)
        {
            var previousChat =  _userChatDb.GetChat(customerId);
            if (previousChat != null)
            {
                var prompt = new StringBuilder();
                prompt.Append($"User Query: {userQuery}");
                prompt.Append($"Chatbot previous response: {previousChat.ChatbotResponse}");
                var bookingResponse =await _llmFormatter.BookingTask(prompt);
                if (bookingResponse != null)
                {
                    if (bookingResponse.IsCompleted == false && bookingResponse.ChatbotResponse != null)
                    {
                        var updatedChat = new UserChatDto()
                        {
                            Intent = previousChat.Intent,
                            UserQuery = userQuery,
                            ChatbotResponse = String.Concat(previousChat.ChatbotResponse, bookingResponse.ChatbotResponse),
                        };
                        _userChatDb.AddChat(customerId,updatedChat);
                        return new ServiceResponse<string>()
                        { 
                            Success = true,
                            Data = bookingResponse.ChatbotResponse
                        };
                    }
                    else if (bookingResponse.IsCompleted == true)
                    {
                        var chatBotFinalResponse = String.Concat(previousChat.ChatbotResponse, bookingResponse.ChatbotResponse);
                       var resultJson = await _llmFormatter.ConvertResponseToJson(chatBotFinalResponse);
                        _userChatDb.RemoveChat(customerId);
                    }
                    else {

                        return new ServiceResponse<string>()
                        {
                            Success = false,
                            //Data = bookingResponse.ChatbotResponse
                            Message = "Please describe a your problem a bit more so that a chatbot can understand and try to solve it",
                        };
                    }

                }
                return new ServiceResponse<string>()
                {
                    Success = false,
                    //Data = bookingResponse.ChatbotResponse
                    Message = "Please describe a your problem a bit more so that a chatbot can understand and try to solve it",
                };
            }
            return new ServiceResponse<string>()
            {
                Success = false,
                //Data = bookingResponse.ChatbotResponse
                Message = "Please describe a your problem a bit more so that a chatbot can understand and try to solve it",
            };
        }

        public async Task<string> ChatbotSubCategoryBooking(ChatbotJsonConverterResponse request)
        {
            var subCategoryBooking = new SubCategoryBookingDTO();
            if (Enum.TryParse<CategoryKeyword>(request.Category, ignoreCase: true, out var kw))
            {
                subCategoryBooking.CategoryId = (int)kw;   
            }
            else 
            {
                return "Invalid category";
            }
            string rawCategory = request.Category;                  
            string subCategory = rawCategory.Replace(" ", "");
            if (Enum.TryParse<SubCategoryKeyword>(subCategory, ignoreCase: true, out var kw1))
            {
                subCategoryBooking.CategoryId = (int)kw1;   
            }
            else
            {
                return "Invalid Sub category";
            }
            var time = request.TimeFrame + '0';
            if (Enum.IsDefined(typeof(TimeFrameKeyword), "_" + time))
            {
               
                var enumName = "_" + time.ToString(); 
                if (Enum.TryParse<TimeFrameKeyword>(enumName, out var kw2))
                {
                    subCategoryBooking.CategoryId = (int)kw2; 
                }
            }
            else
            {
                return "Invalid timeframe";
            }

            return "";
            //// new find the techicians 

        }
    }
}
