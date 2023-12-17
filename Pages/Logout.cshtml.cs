using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ViesbucioPuslapis.Pages
{
    public class LogoutModel : PageModel
    {
        public IActionResult OnGet()
        {
            TempData["SuccessMessage"] = String.Format("Sėkmingai atsijungta");
            HttpContext.Session.Clear();
            return Redirect("/Index");
        }
    }
}
