using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace MuseMarket.Services
{
    public class ImageComparisonService : IImageComparisonService
    {
        private readonly HttpClient _httpClient;

        public ImageComparisonService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> CompareImageAsync(byte[] imageBytes)
        {
            try
            {
                var content = new MultipartFormDataContent();

                // Create ByteArrayContent from the image bytes
                var byteContent = new ByteArrayContent(imageBytes);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

                // Add the file content to the form data
                content.Add(byteContent, "file", "uploaded-image");

                // Send the POST request to the API
                var response = await _httpClient.PostAsync("api/ImageCompare", content);

                // Check if the response was successful
                if (response.IsSuccessStatusCode)
                {
                    // Read the JSON response
                    var json = await response.Content.ReadFromJsonAsync<JsonElement>();

                    // Extract the closest image information from the JSON
                    if (json.TryGetProperty("closestImage", out var closestImageProperty))
                    {
                        return closestImageProperty.GetString();
                    }
                    else
                    {
                        throw new Exception("Unexpected response format: 'closestImage' property not found.");
                    }
                }
                else
                {
                    // Log response status code if the request was not successful
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Failed to compare image. Status Code: {response.StatusCode}, Error: {errorMessage}");
                }
            }
            catch (Exception ex)
            {
                // Log any exception that occurs during the request
                throw new Exception("Error comparing image: " + ex.Message);
            }
        }
    }
}
