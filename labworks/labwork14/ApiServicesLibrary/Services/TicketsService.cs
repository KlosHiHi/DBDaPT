using CinemaClassLibrary.Models;
using System.Text.Json;

namespace ApiServicesLibrary.Services
{
    public class TicketsService(HttpClient client)
    {
        private HttpClient _client = client;
        JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true,
        };

        public async Task<List<Ticket>> GetAsync()
        {
            var response = await _client.GetAsync("Tickets");
            var content = await response.Content.ReadAsStreamAsync();

            return await JsonSerializer.DeserializeAsync<List<Ticket>>(content, _jsonOptions) ?? null!;
        }

        public async Task<Ticket> GetByIdAsync(int id)
        {
            var response = await _client.GetAsync($"Tickets/{id}");
            var content = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<Ticket>(content, _jsonOptions) ?? null!;
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                using var response = await _client.DeleteAsync($"Tickets/{id}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
