using Azure;
using CinemaClassLibrary.Models;
using System.Net;
using System.Text;
using System.Text.Json;

namespace ApiServicesLibrary.Services
{
    public class VisitorService(HttpClient client)
    {
        private HttpClient _client = client;
        JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true,
        };

        public async Task<List<Visitor>?> GetAsync()
        {
            var responce = await _client.GetAsync("Visitors") ?? null!;

            return responce.StatusCode switch
            {
                HttpStatusCode.OK => await JsonSerializer.DeserializeAsync<List<Visitor>>
                    (await responce.Content.ReadAsStreamAsync(), _jsonOptions),
                HttpStatusCode.NoContent => new List<Visitor>(),
                HttpStatusCode.NotFound => throw new HttpRequestException("Ресурс не найден"),
                _ when !responce.IsSuccessStatusCode => throw new HttpRequestException("Ответ неуспешный"),
                _ => null
            };
        }

        public async Task<Visitor?> GetAsyncById(int id)
        {
            var responce = await _client.GetAsync($"Visitors/{id}") ?? null!;
            return responce.StatusCode switch
            {
                HttpStatusCode.NotFound => null,
                _ when !responce.IsSuccessStatusCode => throw new HttpRequestException("Ответ неуспешный"),
                _ => await JsonSerializer.DeserializeAsync<Visitor>
                    (await responce.Content.ReadAsStreamAsync(), _jsonOptions)
            };
        }

        public async Task<bool> PutAsync(Visitor visitor)
        {
            var json = JsonSerializer.Serialize(visitor, _jsonOptions);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var responce = await _client.PutAsync($"Visitors/{visitor.VisitorId}", content);

            return responce.StatusCode switch
            {
                HttpStatusCode.NotFound => false,
                HttpStatusCode.BadRequest => throw new HttpRequestException("Некорректный запрос"),
                _ when !responce.IsSuccessStatusCode => throw new HttpRequestException("Ответ неуспешный"),
                _ when responce.IsSuccessStatusCode => true,
                _ => false
            };
        }

        public async Task<Visitor?> PostAsync(Visitor visitor)
        {
            var json = JsonSerializer.Serialize(visitor, _jsonOptions);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var responce = await _client.PostAsync($"Visitors/", content);

            return responce.StatusCode switch
            {
                HttpStatusCode.BadRequest => throw new HttpRequestException("Некорректный запрос"),
                HttpStatusCode.Conflict => throw new HttpRequestException("Конфликт запроса"),
                _ when !responce.IsSuccessStatusCode => throw new HttpRequestException("Ответ неуспешный"),
                _ when responce.IsSuccessStatusCode => await JsonSerializer.DeserializeAsync<Visitor>(await responce.Content.ReadAsStreamAsync(), _jsonOptions),
                _ => null
            };
        }
    }
}
