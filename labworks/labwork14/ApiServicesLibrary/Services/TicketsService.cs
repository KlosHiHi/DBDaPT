using CinemaClassLibrary.Models;
using System.Text;
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
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Ticket>>(content, _jsonOptions) ?? null!;
        }

        public async Task<Ticket> GetByIdAsync(int id)
        {
            var response = await _client.GetAsync($"Tickets/{id}");
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Ticket>(content, _jsonOptions) ?? null!;
        }

        public async Task PutAsync(Ticket ticket)
        {
            var json = JsonSerializer.Serialize(ticket, _jsonOptions);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PutAsync($"Tickets/{ticket.TicketId}", content);

            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task<string> PostAsync(Ticket ticket)
        {
            var json = JsonSerializer.Serialize(ticket, _jsonOptions);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync($"Tickets", content);

            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            if (response.IsSuccessStatusCode)
            {
                string responseJson = await response.Content.ReadAsStringAsync();
            }

            return await response.Content.ReadAsStringAsync() ?? null!;
        }

        public async Task DeleteAsync(int id)
        {
            var response = await _client.DeleteAsync($"Tickets/{id}");
        }
    }
}
