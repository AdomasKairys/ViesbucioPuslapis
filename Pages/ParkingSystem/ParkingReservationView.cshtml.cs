using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Globalization;
using ViesbucioPuslapis.Data;
using ViesbucioPuslapis.Models;

namespace ViesbucioPuslapis.Pages
{
    public class ParkingReservationViewModel : PageModel
    {
        private readonly ILogger<ParkingReservationViewModel> _logger;
        private readonly HotelDbContext _db;

        public List<Reservation> reservations { get; set; }
        public List<ParkingReservation> parkingReservations { get; set; }
        public List<ParkingPlace> places { get; set; }

        public ParkingReservationViewModel(ILogger<ParkingReservationViewModel> logger, HotelDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public void OnGet()
        {
            CultureInfo myCI = new CultureInfo("lt-LT");
            Calendar myCal = myCI.Calendar;
            CalendarWeekRule myCWR = myCI.DateTimeFormat.CalendarWeekRule;
            DayOfWeek myFirstDOW = myCI.DateTimeFormat.FirstDayOfWeek;

            var user = HttpContext.Session.GetComplexData<User>("user");

            if(user == null)
            {
                return;
            }

            int userid = user.id_Naudotojas;
            reservations = _db.kambario_rezervacija.Where(res => res.fk_Klientas_id_Naudotojas == userid).ToList();
            parkingReservations = new List<ParkingReservation>();
            places = new List<ParkingPlace>();

            foreach(Reservation reservation in reservations)
            {
                parkingReservations.AddRange(_db.stovejimo_vietos_rezervacija.Where(res => res.fk_Kambario_rezervacijaid_Kambario_rezervacija == reservation.id_Kambario_rezervacija).ToList());
            }

            foreach(ParkingReservation reservation in parkingReservations)
            {
                places.Add(_db.stovejimo_vieta.Where(res => res.vietos_id == reservation.fk_Stovejimo_vietavietos_id).FirstOrDefault());
            }

        }

        public IActionResult OnPost(string formId, int resid)
        {
            // Use the formId to identify the form
            if (formId == "deleteReservation")
            {
                return Redirect("./CancelParkingReservation?resId=" + resid);
            }
            else if (formId == "editReservation")
            {
                return Redirect("./EditReservation?resId=" + resid);
            }

            return BadRequest();
        }

    }
}
