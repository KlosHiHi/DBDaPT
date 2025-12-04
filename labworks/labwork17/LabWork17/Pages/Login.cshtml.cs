using AuthLibrary.Contexts;
using AuthLibrary.Models;
using AuthLibrary.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LabWork17.Pages
{
    public class LoginModel(CinemaUserDbContext context, AuthService authService) : PageModel
    {
        private readonly CinemaUserDbContext _context = context;
        private readonly AuthService _authService = authService;

        public IActionResult OnGet()
        {
            return Page();
        }

        public IActionResult OnGetLogout()
        {
            HttpContext.Session.Clear();
            return RedirectToPage("/Index");
        }

        [BindProperty]
        public CinemaUser CinemaUser { get; set; } = default!;

        public async Task<IActionResult> OnPostLoginAsync()
        {

            var user = await _authService.AuthUserAsync(CinemaUser.Login, CinemaUser.PasswordHash);
            var role = await _authService.GetUserRoleAsync(user.Login);

            if (user is null)
                return Page();

            HttpContext.Session.SetString("Login", user.Login);
            HttpContext.Session.SetString("Role", role.RoleName);

            return RedirectToPage("/Films/Index");
        }

        public async Task<IActionResult> OnPostGuest()
        {
            HttpContext.Session.Clear();

            HttpContext.Session.SetString("Role", "Guest");

            return RedirectToPage("/Films/Index");
        }
    }
}
