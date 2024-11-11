using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TechnoSewaMaui.Model;
using TechnoSewaMaui.Response;

namespace TechnoSewaMaui.Services.Auth.Register
{
    public class RegisterServices
    {
        private readonly HttpClient _httpClient;

        public RegisterServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task RequestForOtp(string PhoneNmber) { }

        public async Task VerifyOtp(string model) { }

        public async Task<bool> RegisterUser(UserRegisterRequest UserRequest)
        {
            try
            {
                var url = App.Settings.ApiBaseUrl + "/api/Auth/register";

                var json = JsonConvert.SerializeObject(UserRequest);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(url, content);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
