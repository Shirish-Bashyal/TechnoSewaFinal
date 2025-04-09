using System.Net.Http.Headers;
using System.Net.Http.Json;
using CommunityToolkit.Mvvm.ComponentModel;
using TechnoSewaMaui;
using TechnoSewaMaui.Model;

namespace TechnoSewaMaui.Services.Home
{
    public class ProfileService : ObservableObject, IDisposable
    {
        private static ProfileService _instance;
        private readonly HttpClient _httpClient;
        private ProfileModel _currentProfile;

        // Singleton instance (lazy initialization)
        public static ProfileService Instance => _instance ??= new ProfileService(new HttpClient());

        public ProfileModel CurrentProfile
        {
            get => _currentProfile;
            private set
            {
                if (_currentProfile != value)
                {
                    _currentProfile = value;
                    OnPropertyChanged(); // Notify changes
                }
            }
        }

        // Private constructor for singleton
        private ProfileService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ApiResponse<ProfileModel>> GetUserProfile()
        {
            var jwtToken = await SecureStorage.GetAsync("token");
            if (jwtToken == null)
            {
                return new ApiResponse<ProfileModel>
                {
                    Success = false,
                    Message = "Authentication failed",
                    Data = null
                };
            }

            var url = App.Settings.ApiBaseUrl + "/api/Consumer/profile";
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Bearer",
                jwtToken
            );

            try
            {
                var response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadFromJsonAsync<
                        ApiResponse<ProfileModel>
                    >();

                    if (apiResponse?.Success == true)
                    {
                        CurrentProfile = apiResponse.Data; // Update global profile
                    }

                    return apiResponse
                        ?? new ApiResponse<ProfileModel>
                        {
                            Success = false,
                            Message = "Invalid response format"
                        };
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    return new ApiResponse<ProfileModel>
                    {
                        Success = false,
                        Message = $"Error: {response.StatusCode} - {errorMessage}",
                        Data = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new ApiResponse<ProfileModel>
                {
                    Success = false,
                    Message = $"Network error: {ex.Message}",
                    Data = null
                };
            }
        }

        public void ClearProfile()
        {
            CurrentProfile = null;
        }

        public void Dispose()
        {
            _httpClient?.Dispose();
        }
    }
}
