using ApiServicesLibrary.Services;
using CinemaClassLibrary.Models;

var client = new HttpClient();
string baseUrl = "http://localhost:5256/api/";
client.BaseAddress = new Uri(baseUrl);

//await FilmServiceTest(client);

//await TicketServiceTest(client);

//await GenreServiceTest(client);

await VisitorServiceTest(client);

Console.ReadLine();

static async Task VisitorServiceTest(HttpClient client)
{
    VisitorService visitorService = new(client);

    var visitors = await visitorService.GetAsync();
    var visitor = await visitorService.GetAsyncById(1);
}

static async Task GenreServiceTest(HttpClient client)
{
    GenreService genreService = new(client);

    Genre genre = new()
    {
        GenreId = 10,
        Name = "Авторский фильм"
    };

    await genreService.PutAsync(genre);

    Genre fantaze = new()
    {
        Name = "Фэнтези"
    };

    var insertetGenre = await genreService.PostAsync(fantaze);
    Console.WriteLine($"[{insertetGenre.GenreId}] {insertetGenre.Name}");
}

static async Task TicketServiceTest(HttpClient client)
{
    TicketsService ticketService = new(client);

    var tickets = await ticketService.GetAsync();
    var ticket = await ticketService.GetByIdAsync(1);
}

static async Task FilmServiceTest(HttpClient client)
{
    FilmService filmService = new(client);

    var films = await filmService.GetAsync();
    var film = await filmService.GetByIdAsync(6);

    film.Name = "Бёрдман";
    film.Description = "Фильм о человеке-птице";
    film.Duration = 115;
    film.ReleaseYear = 2012;

    await filmService.PutAsync(film);

    Film сartoon = new()
    {
        Name = "Еретики в Париже",
        Description = "В Париже завелась группа людей, нарушающих правила церкви",
        Duration = 120,
        AgeLimit = "18",
        ReleaseYear = 1998,
        IsDeleted = false
    };

    var inseredFilm = await filmService.PostAsync(сartoon);

    Console.WriteLine($"{inseredFilm.Name} \"{inseredFilm.Description}\" {inseredFilm.AgeLimit}+");
    //await filmService.DeleteAsync(9);
}