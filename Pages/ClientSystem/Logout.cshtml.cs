using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;

namespace ViesbucioPuslapis.Pages.ClientSystem
{
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;

        public LogoutModel(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public IActionResult OnGet()
        {
            // This action will handle GET requests to the Logout page.
            // You can customize it if needed.
            return RedirectToPage("/Index");
        }

        public IActionResult OnPost()
        {
            // Sign out the user
            _signInManager.SignOutAsync();

            // Redirect to the home page or another page after logout
            return RedirectToPage("/Index");
        }
    }
}
