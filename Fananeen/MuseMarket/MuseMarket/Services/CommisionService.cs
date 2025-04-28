using MuseMarket.Models;
using System.Net.Http.Json;

namespace MuseMarket.Services
{
    public class CommisionService
    {
        private readonly HttpClient _httpClient;

        public CommisionService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Get all commissions
        public async Task<List<Commision>> GetCommisionsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Commision>>("api/Commisions");
        }

        // Add a new commission
        public async Task<Commision> CreateCommisionAsync(Commision commision)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Commisions", commision);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Commision>();
            }
            return null;
        }
        public async Task<bool> ToggleDoneAsync(int id)
        {
            var response = await _httpClient.PutAsync($"api/commissions/{id}/ToggleDone", null);

            // Return true if successful, false otherwise
            return response.IsSuccessStatusCode;
        }
        // Delete a commission
        public async Task<bool> DeleteCommisionAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/Commisions/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
