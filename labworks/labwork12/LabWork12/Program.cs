using LabWork12.Contexts;
using LabWork12.Services;
using LabWork12.Sorts;


using CinemaDbContext context = new();

FilmService filmService = new(context);
SessionService sessionService = new(context);
TicketService ticketService = new(context);
VisitorService visitorService = new(context);

Sort sort = new()
{
    ColumnName = "Name",
    isDescending = false
};

Console.WriteLine("task - 1");

// Task 1
var films = await filmService.GetAllOrderedAsync(sort);
films.ForEach(f => Console.WriteLine($"{f.Name} {f.Duration} минут {f.AgeLimit}+"));

Console.WriteLine("\ntask - 2");

// Task 2
films = await filmService.GetByNameAndReleaseYearAsync("Самый лучший фильм", 2012);
films.ForEach(f => Console.WriteLine($"{f.Name} - {f.ReleaseYear} год."));

Console.WriteLine("\ntask - 3");

// Task 3
//var result = await sessionService.IncreasePriceAsync(100, 8);
//Console.WriteLine($"Изменено {result} строк");

Console.WriteLine("\ntask - 4");

// Task 4
var filmGenres = await filmService.GetFilmGenresByIdAsync(3);
filmGenres.ForEach(Console.WriteLine);


Console.WriteLine("\ntask - 5");

// Task 5
var sessionTimes = await ticketService.GetSessionDateByTicketIdAsync(10);
Console.WriteLine(sessionTimes);

Console.WriteLine("\ntask - 6");

// Task 6
char firstChar = 'а';
char secondChar = 'д';
films = await filmService.GetFilmStartWithRangeAsync(firstChar, secondChar);
films.ForEach(f => Console.WriteLine($"{f.Name} {f.Duration} минут {f.AgeLimit}+"));

Console.WriteLine();

var minPrice = await sessionService.GetMinFilmPriceAsync(3);
var maxPrice = await sessionService.GetMaxFilmPriceAsync(3);
var avgPrice = await sessionService.GetAverageFilmPriceAsync(3);

Console.WriteLine($"min - {minPrice}, max - {maxPrice}, avg - {avgPrice}");

Console.WriteLine("\ntask - 7");

// Task 7
string number = "71234567899";
var ticket = await ticketService.GetTicketByPhone(number);
ticket.ForEach(t => Console.WriteLine($"{number} [{t.TicketId}] {t.Row} ряд, {t.Seat} место"));

Console.WriteLine("\ntask - 8");

// Task 8 
//number = "79550776521";
//var newVisitorId = await visitorService.AddVisitor(number);
//Console.WriteLine($"id нового пользователя - {newVisitorId}");

Console.WriteLine("\ntask - 9");

var sessions = await sessionService.GetSessionsByFilmIdAsync(1);
sessions.ForEach(s => Console.WriteLine($"[{s.FilmId}] {s.SessionId} - {s.Price} руб"));