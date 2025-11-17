using CinemaClassLibrary.Models;
using System.Text;
using System.Text.Json;

namespace ApiServicesLibrary.Services
{
    public class GenreService(HttpClient client)
    {
        private HttpClient _client = client;
        JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true,
        };

        public async Task PutAsync(Genre genre)
        {
            try
            {
                var json = JsonSerializer.Serialize(genre, _jsonOptions);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _client.PutAsync($"Genres/{genre.GenreId}", content);

                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task<Genre?> PostAsync(Genre genre)
        {
            try
            {
                var json = JsonSerializer.Serialize(genre, _jsonOptions);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _client.PostAsync($"Genres", content);

                response.EnsureSuccessStatusCode();

                return response.IsSuccessStatusCode ?
                    await JsonSerializer.DeserializeAsync<Genre>(await response.Content.ReadAsStreamAsync(), _jsonOptions) :
                    null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
    }
}
