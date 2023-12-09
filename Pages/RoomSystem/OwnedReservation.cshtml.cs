using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ViesbucioPuslapis.Data;
using ViesbucioPuslapis.Models;

namespace ViesbucioPuslapis.Pages.RoomSystem
{
    public class OwnedReservationModel : PageModel
    {
        private readonly ILogger<ErrorModel> _logger;
        private readonly HotelDbContext _db;
        public List<Reservation> reservations { get; set; }

        public OwnedReservationModel(ILogger<ErrorModel> logger, HotelDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public void OnGet(int id)
        {
            reservations = _db.kambario_rezervacija.Where(res => res.fk_Klientas_id_Naudotojas == id).ToList();
        }
    }
}
