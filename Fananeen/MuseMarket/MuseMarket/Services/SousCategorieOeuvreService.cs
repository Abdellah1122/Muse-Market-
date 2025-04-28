using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using MuseMarket.Models;
namespace MuseMarket.Services
{
	public class SousCategorieOeuvreService
	{
		private readonly HttpClient _httpClient;

		public SousCategorieOeuvreService(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		// Get all sous-categories of oeuvres
		public async Task<List<SousCategorieOeuvre>> GetSousCategoriesOeuvreAsync()
		{
			return await _httpClient.GetFromJsonAsync<List<SousCategorieOeuvre>>("api/SousCategorieOeuvres");
		}

		// Get a specific sous-categorie of oeuvre by ID
		public async Task<SousCategorieOeuvre> GetSousCategorieOeuvreByIdAsync(int id)
		{
			return await _httpClient.GetFromJsonAsync<SousCategorieOeuvre>($"api/SousCategorieOeuvres/{id}");
		}

		// Add a new sous-categorie of oeuvre
		public async Task<SousCategorieOeuvre> AddSousCategorieOeuvreAsync(SousCategorieOeuvre sousCategorieOeuvre)
		{
			var response = await _httpClient.PostAsJsonAsync("api/SousCategorieOeuvres", sousCategorieOeuvre);

			if (response.IsSuccessStatusCode)
			{
				return await response.Content.ReadFromJsonAsync<SousCategorieOeuvre>();
			}

			return null; // Handle error if needed
		}

		// Update an existing sous-categorie of oeuvre
		public async Task<bool> UpdateSousCategorieOeuvreAsync(int id, SousCategorieOeuvre sousCategorieOeuvre)
		{
			var response = await _httpClient.PutAsJsonAsync($"api/SousCategorieOeuvres/{id}", sousCategorieOeuvre);

			return response.IsSuccessStatusCode;
		}

		// Delete a sous-categorie of oeuvre by ID
		public async Task<bool> DeleteSousCategorieOeuvreAsync(int id)
		{
			var response = await _httpClient.DeleteAsync($"api/SousCategorieOeuvres/{id}");

			return response.IsSuccessStatusCode;
		}
	}
}