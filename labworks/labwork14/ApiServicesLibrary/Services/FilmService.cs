using CinemaClassLibrary.Models;
using System.Net.Http.Json;

namespace ApiServicesLibrary.Services
{
    public class FilmService(HttpClient client)
    {
        private HttpClient _client = client;
        public async Task<List<Film>> GetAsync()
            => await _client.GetFromJsonAsync<List<Film>>("Films") ?? null!;

        public async Task<Film> GetByIdAsync(int id)
            => await _client.GetFromJsonAsync<Film>($"Films/{id}") ?? null!;

        public async Task PutAsync(Film film)
        {
            using var response = await _client.PutAsJsonAsync($"Films/{film.FilmId}", film);
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task<Film> PostAsync(Film film)
        {
            using var response = await _client.PostAsJsonAsync($"Films", film);
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return await response.Content.ReadFromJsonAsync<Film>() ?? null!;
        }

        public async Task DeleteAsync(int id)
        {
            using var response = await client.DeleteAsync($"Films/{id}");
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
