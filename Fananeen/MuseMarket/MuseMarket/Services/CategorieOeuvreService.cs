using System.Net.Http.Json;
using MuseMarket.Models;

namespace MuseMarket.Services
{
	public class CategorieOeuvreService
	{
		private readonly HttpClient _httpClient;

		public CategorieOeuvreService(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		// GET: api/CategorieOeuvres
		public async Task<List<CategorieOeuvre>> GetCategoriesOeuvreAsync()
		{
			var response = await _httpClient.GetFromJsonAsync<List<CategorieOeuvre>>("api/CategorieOeuvres");
			return response;
		}

		// GET: api/CategorieOeuvres/5
		public async Task<CategorieOeuvre> GetCategorieOeuvreAsync(int id)
		{
			var response = await _httpClient.GetFromJsonAsync<CategorieOeuvre>($"api/CategorieOeuvres/{id}");
			return response;
		}

		// POST: api/CategorieOeuvres
		public async Task AddCategorieOeuvreAsync(CategorieOeuvre categorieOeuvre)
		{
			var response = await _httpClient.PostAsJsonAsync("api/CategorieOeuvres", categorieOeuvre);
			response.EnsureSuccessStatusCode();
		}

		// PUT: api/CategorieOeuvres/5
		public async Task UpdateCategorieOeuvreAsync(int id, CategorieOeuvre categorieOeuvre)
		{
			var response = await _httpClient.PutAsJsonAsync($"api/CategorieOeuvres/{id}", categorieOeuvre);
			response.EnsureSuccessStatusCode();
		}

		// DELETE: api/CategorieOeuvres/5
		public async Task DeleteCategorieOeuvreAsync(int id)
		{
			var response = await _httpClient.DeleteAsync($"api/CategorieOeuvres/{id}");
			response.EnsureSuccessStatusCode();
		}
	}
}
