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
    SessionId = 4,
    VisitorId = 8,
    Row = 5,
    Seat = 10
};

//await visitorService.AddAsync(newVisitor);
await ticketService.AddAsync(newTicket);

//await ticketService.DeleteAsync(12);
//await visitorService.DeleteAsync(20);

Visitor updateVisitor = new()
{
    VisitorId = 23,
    Phone = "79210775692",
    BirthDate = DateTime.Now,
    Email = "slipknotOff@mail.ru"
};

newTicket.Row = 8;
newTicket.Seat = 15;

//await visitorService.UpdateAsync(updateVisitor);
await ticketService.UpdateAsync(newTicket);
