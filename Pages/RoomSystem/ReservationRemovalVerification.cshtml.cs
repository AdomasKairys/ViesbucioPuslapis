using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ViesbucioPuslapis.Data;
using ViesbucioPuslapis.Models;

namespace ViesbucioPuslapis.Pages.RoomSystem
{
    public class ReservationRemovalVerificationModel : PageModel
    {
        private readonly ILogger<ErrorModel> _logger;
        private readonly HotelDbContext _db;
        public List<Reservation> reservations { get; set; }

        public ReservationRemovalVerificationModel(ILogger<ErrorModel> logger, HotelDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult DeleteReservation()
        {
            //Get reservation through user / reservation id
            //SQL deletion query
            //Throw alert
            //Redirect user

            return RedirectToPage("/Index");
        }

        public void OnGet()
        {
        }
    }
}
