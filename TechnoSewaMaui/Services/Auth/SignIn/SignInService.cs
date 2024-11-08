using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TechnoSewaMaui.Model;
using TechnoSewaMaui.Response;

namespace TechnoSewaMaui.Services.Auth.SignIn
{
    public class SignInService
    {
        private readonly HttpClient _httpClient;

        public SignInService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> SignInUser(UserSignInRequest model)
        {
            try
            {
                var url = $"{App.Settings.ApiBaseUrl}/api/Auth/signIn";
                var json = JsonConvert.SerializeObject(model);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<UserSignInResponse>();
                    if (result.Success)
                    {
                        await SecureStorage.SetAsync("token", result.Data);
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
