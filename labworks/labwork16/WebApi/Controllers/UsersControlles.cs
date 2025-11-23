using CinemaDbLibrary.DTOs;
using CinemaDbLibrary.Models;
using CinemaDbLibrary.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersControlles(AuthService service) : ControllerBase
    {
        private readonly AuthService _service = service;

        [Authorize(Roles = "Администратор")]
        [HttpPost]
        public async Task<ActionResult<CinemaUser>> PostUser(LoginRequest request)
            => await _service.RegistrateUserAsync(request) ?
                Created() :
                BadRequest();
    }
}
