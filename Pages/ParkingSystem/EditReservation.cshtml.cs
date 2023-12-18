using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ViesbucioPuslapis.Data;
using ViesbucioPuslapis.Models;

namespace ViesbucioPuslapis.Pages
{
    public class EditReservationModel : PageModel
    {
        private readonly ILogger<EditReservationModel> _logger;
        private readonly HotelDbContext _db;

        [BindProperty]
        public DateTime startDate { get; set; }
        [BindProperty]
        public DateTime endDate { get; set; }


        public EditReservationModel(ILogger<EditReservationModel> logger, HotelDbContext db)
        {
            _logger = logger;
            _db = db;
        }
        public void OnGet([FromQuery(Name = "resId")] int resid)
        {
            var user = HttpContext.Session.GetComplexData<User>("user");

            if (user == null)
            {
                return;
            }

            int userid = user.id_Naudotojas;

            ParkingReservation reservation = _db.stovejimo_vietos_rezervacija.Where(res => res.id_Stovejimo_vietos_rezervacija == resid).FirstOrDefault();
            startDate = reservation.stovejimo_vietos_pradžia;
            endDate = reservation.stovejimo_vietos_pabaiga;

            ViewData["DefaultStartDate"] = startDate;
            ViewData["DefaultEndDate"] = endDate;
        }

        public IActionResult OnPost([FromQuery(Name = "resId")] int resid)
        {
            ParkingReservation reservation = _db.stovejimo_vietos_rezervacija.Where(res => res.id_Stovejimo_vietos_rezervacija == resid).FirstOrDefault();
            ParkingPlace place = _db.stovejimo_vieta.Where(res => res.vietos_id == reservation.fk_Stovejimo_vietavietos_id).FirstOrDefault();
            reservation.stovejimo_vietos_pradžia = startDate;
            reservation.stovejimo_vietos_pabaiga = endDate;
            place.užimta_nuo = startDate;
            place.užimta_iki = endDate;
            _db.Update(reservation);
            _db.Update(place);
            _db.SaveChanges();
            return Redirect("./ParkingReservationView");
        }
    }
}
