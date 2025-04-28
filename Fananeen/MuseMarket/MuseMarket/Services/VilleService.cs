using MuseMarket.Models;
using System.Net.Http.Json;

namespace MuseMarket.Services
{
    public class VilleService
    {
        private readonly HttpClient _httpClient;

        public VilleService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // GET: api/Villes
        public async Task<List<Ville>> GetVillesAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Ville>>("api/Villes");
        }

        // GET: api/Villes/5
        public async Task<Ville> GetVilleByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Ville>($"api/Villes/{id}");
        }

        
    }
}
