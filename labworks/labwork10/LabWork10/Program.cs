using LabWork10.Contexts;
using LabWork10.Filters;
using LabWork10.Pagination;
using LabWork10.Services;


AppDbContext context = new AppDbContext();

FilmService filmService = new(context);
VisitorService visitorService = new(context);
TicketService ticketService = new(context);
GenreService genreService = new(context);

PageInfo pageInfo = new();

FilmFilter filmFilter = new()
{
    NamePart = "н",
};

var tickets = await ticketService.GetAsync(pageInfo: pageInfo);
Console.WriteLine("Билеты:");
foreach (var ticket in tickets)
    Console.WriteLine($"[{ticket.SessionId}][{ticket.TicketId}] - {ticket.Row} ряд. {ticket.Seat} место.");

Console.WriteLine();

var visitors = await visitorService.GetAsync(isDescending: true);
Console.WriteLine("Посетители:");
foreach (var visitor in visitors)
    Console.WriteLine($"{visitor.Name}, {visitor.Phone}");

Console.WriteLine();

var films = await filmService.GetAsync(filmFilter: filmFilter, isDescending: true);
Console.WriteLine("Фильм:");
foreach (var film in films)
    Console.WriteLine($"{film.Name} {film.AgeLimit}+");

Console.WriteLine();

var visitorsDto = await visitorService.GetDtoAsync();
Console.WriteLine("Билеты Dto:");
foreach (var visitorDto in visitorsDto)
    Console.WriteLine($"{visitorDto?.Phone} билетов: {visitorDto?.TicketsAmount}шт.");

Console.WriteLine();

var filmGenreDtos = await filmService.GetFilmGenreDtoAsync();
Console.WriteLine("Жанры фильмов Dto:");
foreach (var filmGenreDto in filmGenreDtos)
{
    Console.Write($"[{filmGenreDto.FilmId}] - {filmGenreDto.FilmName} жанры: ");
    foreach (var genre in filmGenreDto.Genres)
        Console.Write($"{genre} ");
    Console.WriteLine();
}