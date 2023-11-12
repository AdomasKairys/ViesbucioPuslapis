using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ViesbucioPuslapis.Pages
{
    public class ParkingReservationViewModel : PageModel
    {
        private readonly ILogger<ParkingReservationViewModel> _logger;

        public ParkingReservationViewModel(ILogger<ParkingReservationViewModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
