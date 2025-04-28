using Blazored.LocalStorage;
using MuseMarket.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;

namespace MuseMarket.Services
{
    public class ArtistService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;
        private Artist? _artist;
        private Artist? _ArtistCurrent=null;

        public ArtistService(HttpClient httpClient, ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
        }

        // Current Artist Management
        public async Task<Artist> GetArtist() => _artist ??= await _localStorage.GetItemAsync<Artist>("currentArtist");

        public async Task<Artist> GetCurrentArtist() => _ArtistCurrent ??= await _localStorage.GetItemAsync<Artist>("CurrentArtist");
        public async Task SetArtistCurrent(Artist artist)
        {
            _ArtistCurrent = artist;
            await _localStorage.SetItemAsync("CurrentArtist", artist);
        }
        public async Task ClearArtistCurrent()
        {
            _ArtistCurrent = null;
            await _localStorage.RemoveItemAsync("CurrentArtist");
        }

        public async Task SetArtist(Artist artist)
        {
            _artist = artist;
            await _localStorage.SetItemAsync("currentArtist", artist);
        }

        

        // Authentication
        public async Task<string> LoginAsync(string username, string password)
        {
            // Create a new object with username and password
            var loginData = new { username, password };

            // Serialize the object to JSON
            var content = new StringContent(
                JsonSerializer.Serialize(loginData),
                Encoding.UTF8,
                "application/json"
            );

            // Send the POST request with the JSON body
            var response = await _httpClient.PostAsync("api/artists/Login", content);

            // Handle failed responses
            if (!response.IsSuccessStatusCode)
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                return $"Login failed: {errorMessage}";
            }

            // Deserialize the response to get the result
            var result = await response.Content.ReadFromJsonAsync<LoginResponse>();

            // Check if artist data is found
            if (result?.Artist == null)
            {
                return "Login failed: No artist data found.";
            }

            // Set the current artist and store it in local storage
            await SetArtistCurrent(result.Artist);

            // Return the success message
            return result.Message;
        }



        // CRUD Operations
        public async Task<List<Artist>> GetArtistsAsync() =>
            await _httpClient.GetFromJsonAsync<List<Artist>>("api/artists");

        public async Task<Artist> GetArtistByIdAsync(int id) =>
            await _httpClient.GetFromJsonAsync<Artist>($"api/artists/{id}");

        public async Task<Artist> CreateArtistAsync(Artist artist)
        {
            var response = await _httpClient.PostAsJsonAsync("api/artists", artist);
            return response.IsSuccessStatusCode ? await response.Content.ReadFromJsonAsync<Artist>() : null;
        }

        public async Task<bool> UpdateArtistAsync(int id, Artist artist) =>
            (await _httpClient.PutAsJsonAsync($"api/artists/{id}", artist)).IsSuccessStatusCode;

        public async Task<bool> DeleteArtistAsync(int id) =>
            (await _httpClient.DeleteAsync($"api/artists/{id}")).IsSuccessStatusCode;

        // Artist Modifications
        public async Task<bool> ToggleAvailabilityAsync(int id) =>
            (await _httpClient.PutAsync($"api/artists/{id}/ToggleAvailability", null)).IsSuccessStatusCode;

        public async Task<bool> IncrementNbrFoisVisiteAsync(int id) =>
            (await _httpClient.PutAsync($"api/artists/{id}/IncrementNbrFoisVisite", null)).IsSuccessStatusCode;

        public async Task<bool> IncrementNbrOeuvreVenduAsync(int id) =>
            (await _httpClient.PutAsync($"api/artists/{id}/IncrementNbrOeuvreVendu", null)).IsSuccessStatusCode;

        public async Task<bool> AddRatingAsync(int id, int rating) =>
            (await _httpClient.PutAsync($"api/artists/{id}/AddRating?rating={rating}", null)).IsSuccessStatusCode;

        public async Task<bool> ChangeArtistNameAsync(int id, string newName) =>
            (await _httpClient.PutAsync($"api/artists/{id}/ChangeNom?Nom={newName}", null)).IsSuccessStatusCode;

        public async Task<bool> ChangePrenomAsync(int id, string prenom) =>
            (await _httpClient.PutAsync($"api/artists/{id}/ChangePrenom?Prenom={prenom}", null)).IsSuccessStatusCode;

        public async Task<bool> ChangeBioAsync(int id, string bio) =>
            (await _httpClient.PutAsync($"api/artists/{id}/ChangeBio?Bio={bio}", null)).IsSuccessStatusCode;

        public async Task<bool> ChangeMailAsync(int id, string mail) =>
            (await _httpClient.PutAsync($"api/artists/{id}/ChangeMail?Mail={mail}", null)).IsSuccessStatusCode;

        public async Task<bool> ChangeTelephoneAsync(int id, string telephone) =>
            (await _httpClient.PutAsync($"api/artists/{id}/ChangeTelephone?Telephone={telephone}", null)).IsSuccessStatusCode;
        public async Task<bool> ChangeUseNameAsync(int id, string usename) =>
            (await _httpClient.PutAsync($"api/artists/{id}/ChangeUserName?UserName={usename}", null)).IsSuccessStatusCode;
        public async Task<bool> ChangeVilleAsync(int id, int villeId)
        {
            var response = await _httpClient.PutAsync($"api/artists/{id}/ChangeVille?villeId={villeId}", null);
            return response.IsSuccessStatusCode;
        }
        public async Task<bool> ChangeArtFormAsync(int id, int artFormId) =>
            (await _httpClient.PutAsync($"api/artists/{id}/ChangeArtForm?artFormId={artFormId}", null)).IsSuccessStatusCode;



        

    }

    public class LoginResponse
    {
        public string Message { get; set; }
        public Artist Artist { get; set; }
    }
}
