using CinemaDbLibrary.DTOs;
using CinemaDbLibrary.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthsController(AuthService service) : ControllerBase
    {
        private readonly AuthService _service = service;

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult> RegistrateUser(LoginRequest loginRequest)
            => await _service.RegistrateUserAsync(loginRequest) ?
                Created() :
                BadRequest();

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<TokenResponse>> PostUser(LoginRequest loginRequest)
        {
            var token = await _service.AuthUserAsync(loginRequest);

            if (token is null)
                BadRequest();

            return Ok(token);
        }
    }
}
