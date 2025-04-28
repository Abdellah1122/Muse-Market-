using MuseMarket.Models;
using System.Net.Http.Json;

namespace MuseMarket.Services
{
    public class CommentaireService
    {
        private readonly HttpClient _httpClient;

        public CommentaireService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Get all commentaires
        public async Task<List<Commentaire>> GetCommentairesAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Commentaire>>("api/Commentaires");
        }

        // Add a new commentaire
        public async Task<Commentaire> CreateCommentaireAsync(Commentaire commentaire)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Commentaires", commentaire);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Commentaire>();
            }
            return null;
        }

        // Delete a commentaire
        public async Task<bool> DeleteCommentaireAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/Commentaires/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
