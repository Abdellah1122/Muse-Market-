using MuseMarket.Models;
using System.Net.Http.Json;

namespace MuseMarket.Services
{
    public class VenteService
    {
        private readonly HttpClient _httpClient;

        public VenteService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Vente>> GetVentesAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Vente>>("api/Ventes");
        }

        public async Task<Vente> GetVenteByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Vente>($"api/Ventes/{id}");
        }

        public async Task<bool> UpdateVenteAsync(int id, Vente vente)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/Ventes/{id}", vente);
            return response.IsSuccessStatusCode;
        }

        public async Task<Vente> CreateVenteAsync(Vente vente)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Ventes", vente);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Vente>();
            }
            return null;
        }

        public async Task<bool> DeleteVenteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/Ventes/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}