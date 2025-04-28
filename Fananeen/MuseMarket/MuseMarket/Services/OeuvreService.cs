using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using MuseMarket.Models;
namespace MuseMarket.Services
{
	public class OeuvreService
	{
		private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;
        public Oeuvre? _oeuvre;

        public OeuvreService(HttpClient httpClient, ILocalStorageService localStorage)
		{
			_httpClient = httpClient;
            _localStorage = localStorage;
        }
        //
        // Current Artist 
        public async Task<Oeuvre> GetOeuvre()
        {
            if (_oeuvre == null)
            {
                _oeuvre = await _localStorage.GetItemAsync<Oeuvre>("currentOeuvre");
            }
            return _oeuvre;
        }

        public async Task SetOeuvre(Oeuvre oeuvre)
        {
            _oeuvre = oeuvre;
            await _localStorage.SetItemAsync("currentOeuvre", oeuvre);
        }
        //
        public async Task<bool> ToggleAvailabilityAsync(int id, StatutOeuvre statutOeuvre) =>
			(await _httpClient.PutAsync($"api/Oeuvres/{id}/ToggleAvailability?statu={(int)statutOeuvre}", null)).IsSuccessStatusCode;
        
        

        // Get all oeuvres
        public async Task<List<Oeuvre>> GetOeuvresAsync()
		{
			return await _httpClient.GetFromJsonAsync<List<Oeuvre>>("api/Oeuvres");
		}

		// Get a single oeuvre by ID
		public async Task<Oeuvre> GetOeuvreByIdAsync(int id)
		{
			return await _httpClient.GetFromJsonAsync<Oeuvre>($"api/Oeuvres/{id}");
		}

		// Add a new oeuvre
		public async Task<Oeuvre> AddOeuvreAsync(Oeuvre oeuvre)
		{
			var response = await _httpClient.PostAsJsonAsync("api/Oeuvres", oeuvre);

			if (response.IsSuccessStatusCode)
			{
				return await response.Content.ReadFromJsonAsync<Oeuvre>();
			}

			return null; // Handle errors in your real application
		}

		// Update an existing oeuvre
		public async Task<bool> UpdateOeuvreAsync(int id, Oeuvre oeuvre)
		{
			var response = await _httpClient.PutAsJsonAsync($"api/Oeuvres/{id}", oeuvre);

			return response.IsSuccessStatusCode;
		}

		// Delete an oeuvre by ID
		public async Task<bool> DeleteOeuvreAsync(int id)
		{
			var response = await _httpClient.DeleteAsync($"api/Oeuvres/{id}");

			return response.IsSuccessStatusCode;
		}
	}
}