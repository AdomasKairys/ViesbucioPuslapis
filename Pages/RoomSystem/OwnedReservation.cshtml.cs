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

        public void OnGet()
        {
            //int userid = 2;
            var user = HttpContext.Session.GetComplexData<User>("user");
            int userid = user.id_Naudotojas;
            reservations = _db.kambario_rezervacija.Where(res => res.fk_Klientas_id_Naudotojas == userid).ToList();
        }

        public IActionResult OnPost(int resid)
        {
            //int userid = 2;
            var user = HttpContext.Session.GetComplexData<User>("user");
            int userid = user.id_Naudotojas;
            reservations = _db.kambario_rezervacija.Where(res => res.fk_Klientas_id_Naudotojas == userid).ToList();

            Reservation reservation = reservations.First(res => res.id_Kambario_rezervacija == resid);

            _db.Remove(reservation);
            _db.SaveChanges();

            TempData["RoomReservationDeleteSuccess"] = string.Format("Kambario {0} rezervacija sėkmingai pašalinta.", reservation.fk_Kambarys_kambario_numeris);

            return Redirect("./OwnedReservation");
        }
    }
}
