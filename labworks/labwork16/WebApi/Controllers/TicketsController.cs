using CinemaDbLibrary.Models;
using CinemaDbLibrary.Options;
using CinemaDbLibrary.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController(TicketService ticketService) : ControllerBase
    {
        private readonly TicketService _ticketService = ticketService;

        [Authorize(Roles = "Посетитель,Билетер")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ticket>>> GetFilms(
            [FromQuery] SortOptions? sortOptions = null,
            [FromQuery] PaginationOptions? pageOptions = null)
        {
            var tickets = await _ticketService.GetAsync(pageOptions, sortOptions);

            return tickets is null ?
                NotFound() :
                tickets.ToList();
        }

        [Authorize(Roles = "Посетитель,Билетер")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Ticket>> GetFilm(int id)
        {
            var ticket = await _ticketService.GetByIdAsync(id);

            return ticket is null ?
                NotFound() :
                ticket;
        }
    }
}
