using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ViesbucioPuslapis.Pages
{
    public class GymIndexModel : PageModel
    {
        private readonly ILogger<GymIndexModel> _logger;

        public GymIndexModel(ILogger<GymIndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
