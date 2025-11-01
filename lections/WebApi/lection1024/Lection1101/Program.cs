using Lection1101.Models;
using System.Net.Http.Json;
using System.Text.Json;

Console.WriteLine("call Web-API!");

var client = new HttpClient();
string baseUrl = "https://api.escuelajs.co/api/v1/";
client.BaseAddress = new Uri(baseUrl);

using var response = await client.GetAsync("categories");
response.EnsureSuccessStatusCode();

var jsonOptions = new JsonSerializerOptions
{
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    WriteIndented = true,
};

var content = await response.Content.ReadAsStringAsync();
var categories = JsonSerializer.Deserialize<List<Category>>(content, jsonOptions);



Console.WriteLine();

static async Task<(HttpResponseMessage response1, HttpResponseMessage response2, HttpResponseMessage response3)> TestApi(HttpClient client)
{
    var categories = await client.GetFromJsonAsync<List<Category>>("categories");

    int id = 41;
    var category = await client.GetFromJsonAsync<Category>($"categories/{id}");

    category = new Category { Name = "georgiy", Image = "https://placeimg.com/640/480/any" };
    using var response1 = await client.PostAsJsonAsync<Category>("categories", category);
    response1.EnsureSuccessStatusCode();

    var result = await response1.Content.ReadFromJsonAsync<Category>();


    category.Name = "newName";

    using var response2 = await client.PutAsJsonAsync($"categories/{category.Id}", category);

    response2.EnsureSuccessStatusCode();

    using var response3 = await client.DeleteAsync($"categories/{category.Id}");
    response3.EnsureSuccessStatusCode();
    return (response1, response2, response3);
}