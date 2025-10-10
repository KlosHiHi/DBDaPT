using LabWork10.Contexts;
using LabWork10.Services;

Console.WriteLine("Hello, World!");

AppDbContext context = new AppDbContext();

FilmService filmService = new(context);
VisitorService visitorService = new(context);
TicketService ticketService = new(context);
GenreService genreService = new(context);

var tickets = await ticketService.GetAsync();

Console.WriteLine();
Console.WriteLine("Билеты:");
foreach (var ticket in tickets)
    Console.WriteLine($"[{ticket.TicketId}] - {ticket.Row} ряд. {ticket.Seat} место.");

Console.WriteLine();
var visitors = await visitorService.GetAsync(5, 2);
foreach (var visitor in visitors)
    Console.WriteLine($"{visitor.Name}, {visitor.Phone}");

Console.WriteLine();
var films = await filmService.GetAsync();
foreach(var film in films)
    Console.WriteLine($"{film.Name}, {film.ReleaseYear} г.");