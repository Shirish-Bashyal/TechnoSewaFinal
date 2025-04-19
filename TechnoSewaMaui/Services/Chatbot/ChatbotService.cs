using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using TechnoSewaMaui.Model;
using TechnoSewaMaui.Response;

namespace TechnoSewaMaui.Services.Chatbot
{
    public class ChatbotService
    {
        private readonly HttpClient _httpClient;
        public ChatbotService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> FormatMessageService(SendMessageModel request)
        {
            try
            {
                // var url = $"{App.Settings.ApiBaseUrl}/api/Auth/signIn";
                var url = App.Settings.ApiBaseUrl + "/api/Chatbot/postQuestions";

                var json = JsonConvert.SerializeObject(request);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    if (result != null)
                    {
                        
                        return result;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    

                    return null;
                }
            }
            catch
            {
                return null;
            }
        }
    }
}
