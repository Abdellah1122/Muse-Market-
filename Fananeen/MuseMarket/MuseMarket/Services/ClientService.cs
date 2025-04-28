using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;
using MuseMarket.Models;
using Blazored.LocalStorage;
namespace MuseMarket.Services
{
	public class ClientService
	{
		private readonly HttpClient _httpClient;

        private readonly ILocalStorageService _localStorage;
        private Client? _ClientLoggedIn;

        public ClientService(HttpClient httpClient, ILocalStorageService localStorage)
		{
			_httpClient = httpClient;

            _localStorage = localStorage;
        }
        public async Task<Client> GetCurrentClient() => _ClientLoggedIn ??= await _localStorage.GetItemAsync<Client>("CurrentClient");

        public async Task SetClient(Client client)
        {
            _ClientLoggedIn = client;
            await _localStorage.SetItemAsync("CurrentClient", client);
        }
        public async Task ClearClient()
        {
            _ClientLoggedIn = null;
            await _localStorage.RemoveItemAsync("CurrentClient");
        }
        // Get all clients
        public async Task<List<Client>> GetClientsAsync()
		{
			return await _httpClient.GetFromJsonAsync<List<Client>>("api/Clients");
		}

		// Get a single client by ID
		public async Task<Client> GetClientByIdAsync(int id)
		{
			return await _httpClient.GetFromJsonAsync<Client>($"api/Clients/{id}");
		}

		// Add a new client
		public async Task<Client> AddClientAsync(Client client)
		{
			var response = await _httpClient.PostAsJsonAsync("api/Clients", client);

			if (response.IsSuccessStatusCode)
			{
				return await response.Content.ReadFromJsonAsync<Client>();
			}

			return null; // Handle error in your real application
		}
        public async Task<string> LoginAsync(string username, string password)
        {
            
            var loginData = new { username, password };

            var content = new StringContent(
                JsonSerializer.Serialize(loginData),
                Encoding.UTF8,
                "application/json"
            );

            var response = await _httpClient.PostAsync("api/clients/Login", content);

            // Handle failed responses
            if (!response.IsSuccessStatusCode)
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                return $"Login failed: {errorMessage}";
            }

            
            var result = await response.Content.ReadFromJsonAsync<LoginResponseO>();

            // Check if artist data is found
            if (result?.client == null)
            {
                return "Login failed: No artist data found.";
            }

             await SetClient(result.client);

            return result.Message;
        }
        // Client Modifications
        public async Task<bool> ChangeNomAsync(int id, string newNom) =>
            (await _httpClient.PutAsync($"api/clients/{id}/ChangeNom?Nom={newNom}", null)).IsSuccessStatusCode;

        public async Task<bool> ChangePrenomAsync(int id, string prenom) =>
            (await _httpClient.PutAsync($"api/clients/{id}/ChangePrenom?Prenom={prenom}", null)).IsSuccessStatusCode;

        public async Task<bool> ChangeMailAsync(int id, string email) =>
            (await _httpClient.PutAsync($"api/clients/{id}/ChangeMail?Mail={email}", null)).IsSuccessStatusCode;

        public async Task<bool> ChangeTelephoneAsync(int id, string telephone) =>
            (await _httpClient.PutAsync($"api/clients/{id}/ChangeTelephone?Telephone={telephone}", null)).IsSuccessStatusCode;
        public async Task<bool> ChngeAddressAsync(int id, string address) =>
            (await _httpClient.PutAsync($"api/clients/{id}/ChangeAddress?address={address}", null)).IsSuccessStatusCode;
        public async Task<bool> ChangePostCode(int id, int postcode) =>
            (await _httpClient.PutAsync($"api/clients/{id}ChangePostCode?postcode={postcode}", null)).IsSuccessStatusCode;
        public async Task<bool> ChangeUsernameAsync(int id, string username) =>
            (await _httpClient.PutAsync($"api/clients/{id}/ChangeUserName?UserName={username}", null)).IsSuccessStatusCode;
        public async Task<bool> ChangePassWord(int id, string password) =>
            (await _httpClient.PutAsync($"api/clients/{id}/ChangePassword?password={password}", null)).IsSuccessStatusCode;

        public async Task<bool> ChangeVilleAsync(int id, int villeId) =>
            (await _httpClient.PutAsync($"api/clients/{id}/ChangeVille?villeId={villeId}", null)).IsSuccessStatusCode;
        public async Task<bool> IncrementNbrCommisionListedAsync(int id)
        {
            var response = await _httpClient.PutAsync($"api/clients/{id}/IncrementNbrCommisionListed", null);
            return response.IsSuccessStatusCode;
        }
        public async Task<bool> IncrementReviewsMade(int id)
        {
            var response = await _httpClient.PutAsync($"api/clients/{id}/IncrementReviewsMade", null);
            return response.IsSuccessStatusCode;
        }
        public async Task<bool> IncrementArtistBooked(int id)
        {
            var response = await _httpClient.PutAsync($"api/clients/{id}/IncrementArtistsBooked", null);
            return response.IsSuccessStatusCode;
        }
        public async Task<bool> IncrementWorksBought(int id)
        {
            var response = await _httpClient.PutAsync($"api/clients/{id}/IncrementWorksBought", null);
            return response.IsSuccessStatusCode;
        }
        // Delete a client by ID
        public async Task<bool> DeleteClientAsync(int id)
		{
			var response = await _httpClient.DeleteAsync($"api/Clients/{id}");

			return response.IsSuccessStatusCode;
		}
	}
    public class LoginResponseO
    {
        public string Message { get; set; }
        public Client client { get; set; }
    }
}