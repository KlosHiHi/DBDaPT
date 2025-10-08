using LabWork9.Contexts;
using LabWork9.Models;
using LabWork9.Services;

using AppDbContext context = new();

VisitorService visitorService = new(context);
TicketService ticketService = new(context);

var visitors = await visitorService.GetAsync();
var tickets = await ticketService.GetAsync();

foreach (var ticket in tickets)
    Console.WriteLine($"[{ticket.TicketId}] - {ticket.Row} ряд, {ticket.Seat}место. Принадлежит пользователю [{ticket.Visitor?.Name}]");

Console.WriteLine();

foreach (var visitor in visitors)
    Console.WriteLine($"{visitor.Name} - {visitor.Phone}");

Visitor newVisitor = new()
{
    Name = "Кори",
    Phone = "15563523344",
    BirthDate = DateTime.Now,
    Email = "slipknotOFF@gmail.com"
};

Ticket newTicket = new()
{
    SessionId = 5,
    VisitorId = 2,
    Row = 6,
    Seat = 13
};

//await visitorService.AddAsync(newVisitor);
//await ticketService.AddAsync(newTicket);

//await ticketService.DeleteAsync(6);
//await visitorService.DeleteAsync(6);

Visitor updateVisitor = new()
{
    VisitorId = 4,
    Phone = "79210775692",
    BirthDate = DateTime.Now,
    Email = "slipknotOff@mail.ru"
};

Ticket updateTicket = new()
{
    TicketId = 1,
    SessionId = 1,
    VisitorId = 2,
    Row = 5,
    Seat = 11
};

await visitorService.UpdateAsync(updateVisitor);
await ticketService.UpdateAsync(updateTicket);