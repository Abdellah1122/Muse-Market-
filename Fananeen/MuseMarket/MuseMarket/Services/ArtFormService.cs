using MuseMarket.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;

using System.Threading.Tasks;

namespace MuseMarket.Services
{
    public class ArtFormService
    {
        private readonly HttpClient _httpClient;

        public ArtFormService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Get all art forms
        public async Task<List<ArtForm>> GetArtFormsAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<List<ArtForm>>("api/artforms");
            return response;
        }

        // Get a specific art form by ID
        public async Task<ArtForm> GetArtFormByIdAsync(int id)
        {
            var response = await _httpClient.GetFromJsonAsync<ArtForm>($"api/artforms/{id}");
            return response;
        }

        // Add a new art form
        public async Task<ArtForm> CreateArtFormAsync(ArtForm artForm)
        {
            var response = await _httpClient.PostAsJsonAsync("api/artforms", artForm);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<ArtForm>();
            }
            return null;
        }

        // Update an existing art form
        public async Task<bool> UpdateArtFormAsync(int id, ArtForm artForm)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/artforms/{id}", artForm);
            return response.IsSuccessStatusCode;
        }

        // Delete an art form
        public async Task<bool> DeleteArtFormAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/artforms/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
