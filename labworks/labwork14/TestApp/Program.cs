using ApiServicesLibrary.Services;
using CinemaClassLibrary.Models;

var client = new HttpClient();
string baseUrl = "http://localhost:5256/api/";
client.BaseAddress = new Uri(baseUrl);

FilmService filmService = new(client);

var films = await filmService.GetAsync();
var film = await filmService.GetByIdAsync(9);

film.Description = "Фильм о человеке-птице";
film.Duration = 115;
film.ReleaseYear = 2012;

await filmService.PutAsync(film);

Film Cartoon = new()
{
    Name = "Еретики в Париже",
    Duration = 120,
    AgeLimit = "18",
    ReleaseYear = 1998,
    IsDeleted = false
};

var inseredFilm = await filmService.PostAsync(Cartoon);

await filmService.DeleteAsync(10);

Console.ReadLine();