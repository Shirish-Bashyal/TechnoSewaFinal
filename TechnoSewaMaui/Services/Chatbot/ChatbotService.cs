using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using TechnoSewaMaui.Model;
using TechnoSewaMaui.Response;
using static Android.Util.EventLogTags;

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


        public async Task<string> FormatAudioService(byte[] audio, string audioFileName)
        {
            try
            {
               
                var url = App.Settings.ApiBaseUrl + "/api/Chatbot/postAudio";


                using (var formData = new MultipartFormDataContent())
                {
                  



                    if (audio != null)
                    {
                        var fileContent = new ByteArrayContent(audio);

                        formData.Add(fileContent, "Audio", audioFileName);
                    }



                    var response = await _httpClient.PostAsync(url, formData);

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
            }
            catch
            {
                return null;
            }

        }
        public async Task<string> FormatMessageWithImageService(SendMessageWithImageModel request)
        {
            try
            {
                // var url = $"{App.Settings.ApiBaseUrl}/api/Auth/signIn";
                var url = App.Settings.ApiBaseUrl + "/api/Chatbot/postImage";


                using (var formData = new MultipartFormDataContent())
                {
                    formData.Add(new StringContent(request.Question), "Text");



                    if (request.ImageFile != null && request.ImageFile.Length > 0)
                    {
                        var fileContent = new ByteArrayContent(request.ImageFile);

                        formData.Add(fileContent, "Image", request.ImageName);
                    }



                    var response = await _httpClient.PostAsync(url, formData);

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
            }
            catch
            {
                return null;
            }
        }
    }
}
