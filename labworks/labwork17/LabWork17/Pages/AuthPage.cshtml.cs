using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LabWork17.Pages
{
    public class AuthPageModel : PageModel
    {
        public string UserRole => HttpContext.Session.GetString("Role");
        public bool IsAdmin => UserRole == "Администратор";


        protected IActionResult HasRole()
        {
            if (String.IsNullOrEmpty(UserRole))
                return RedirectToPage("/Login");

            return null;
        }

        protected IActionResult IsInRole(string role)
        {
           throw new NotImplementedException();
        }
    }
}
