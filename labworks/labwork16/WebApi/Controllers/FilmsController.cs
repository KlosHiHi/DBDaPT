using CinemaDbLibrary.Models;
using CinemaDbLibrary.Options;
using CinemaDbLibrary.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmsController(FilmService service) : ControllerBase
    {
        private readonly FilmService _service = service;

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Film>>> GetFilms(
            [FromQuery] SortOptions? sortOptions = null,
            [FromQuery] PaginationOptions? pageOptions = null)
        {
            var films = await _service.GetAsync(pageOptions, sortOptions);

            return films is null ?
                NotFound() :
                films.ToList();
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<Film>> GetFilm(int id)
        {
            var film = await _service.GetByIdAsync(id);

            return film is null ?
                NotFound() :
                film;
        }
    }
}
