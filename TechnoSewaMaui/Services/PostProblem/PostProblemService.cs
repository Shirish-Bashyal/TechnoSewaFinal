using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Java.Net;
using TechnoSewaMaui.Model;

namespace TechnoSewaMaui.Services.PostProblem
{
    public partial class PostProblemService
    {
        private readonly HttpClient _httpClient;

        public PostProblemService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> PostProblem(
            string Category,
            string Title,
            string Description,
            double latitude,
            double longitude,
            byte[] imageData,
            string imageName
        )
        {
            try
            {
                var jwtToken = await SecureStorage.GetAsync("token");
                if (jwtToken == null)
                {
                    return null;
                }
                var url = $"{App.Settings.ApiBaseUrl}/api/Post/problem";
                using (var formData = new MultipartFormDataContent())
                {
                    formData.Add(new StringContent(Title), "Title");
                    formData.Add(new StringContent(Description), "Description");
                    formData.Add(new StringContent(Category), "Category");

                    formData.Add(
                        new StringContent(latitude.ToString(CultureInfo.InvariantCulture)),
                        "Lattitude"
                    );
                    formData.Add(
                        new StringContent(longitude.ToString(CultureInfo.InvariantCulture)),
                        "Longitude"
                    );

                    //if (ImageFiles != null && ImageFiles.Length > 0)
                    //{
                    //    foreach (var imageFile in ImageFiles)
                    //    {
                    if (imageData != null && imageData.Length > 0)
                    {
                        var fileContent = new ByteArrayContent(imageData);
                        string fileExtension = Path.GetExtension(imageName).ToLower();
                        switch (fileExtension)
                        {
                            case ".jpg":
                            case ".jpeg":
                                fileContent.Headers.ContentType = new MediaTypeHeaderValue(
                                    "image/jpeg"
                                );
                                break;
                            case ".png":
                                fileContent.Headers.ContentType = new MediaTypeHeaderValue(
                                    "image/png"
                                );
                                break;
                            default:

                                Console.WriteLine("Unsupported file type.");
                                return "Image must be .jpg or .jpeg or .png";
                        }

                        formData.Add(fileContent, "ImageFiles", imageName);
                    }

                    //var content = new StringContent(json, Encoding.UTF8, "application/json");
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                        "Bearer",
                        jwtToken
                    );
                    var response = await _httpClient.PostAsync(url, formData);
                    Console.WriteLine($"s Code: {(int)response.StatusCode}");
                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadFromJsonAsync<
                            ApiResponse<object>
                        >();
                        if (result.Success == true)
                        {
                            return result.Message;
                        }
                        else
                        {
                            return result.Message;
                        }
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }

            //send data
        }
    }
}
