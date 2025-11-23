using CinemaDbLibrary.Models;
using CinemaDbLibrary.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfilesController(AuthService service) : ControllerBase
    {
        private readonly AuthService _service = service;

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<CinemaUser>> GetCurrentUserData()
        {
            var claim = User.Claims.FirstOrDefault(u => u.Type == "login");
            var user = await _service.GetUserByLoginAsync(claim.Value);

            return user is null ?
                 NotFound() :
                 user;
        }
    }
}
