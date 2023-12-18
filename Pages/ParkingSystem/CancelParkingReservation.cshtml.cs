using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ViesbucioPuslapis.Data;
using ViesbucioPuslapis.Models;

namespace ViesbucioPuslapis.Pages.RoomSystem
{
    public class CancelParkingReservationModel : PageModel
    {
        private readonly ILogger<CancelParkingReservationModel> _logger;
        private readonly HotelDbContext _db;

        public List<Reservation> reservations { get; set; }
        public List<ParkingReservation> parkingReservations { get; set; }
        public List<ParkingPlace> places { get; set; }

        public CancelParkingReservationModel(ILogger<CancelParkingReservationModel> logger, HotelDbContext db)
        {
            _logger = logger;
            _db = db;
        }
        public void OnGet()
        {
        }

        public IActionResult OnPost([FromQuery(Name = "resId")] int resid)
        {
            //int userid = 2;
            ParkingReservation reservation = _db.stovejimo_vietos_rezervacija.Where(res => res.id_Stovejimo_vietos_rezervacija == resid).FirstOrDefault();
            ParkingPlace place = _db.stovejimo_vieta.Where(res => res.vietos_id == reservation.fk_Stovejimo_vietavietos_id).FirstOrDefault();
            place.uþimta_nuo = DateTime.MinValue;
            place.uþimta_iki = DateTime.MinValue;
            place.vietos_uþimtumas = false;
            _db.Remove(reservation);
            _db.Update(place);
            _db.SaveChanges();

            return Redirect("./ParkingReservationView");

        }
    }
}
