using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Globalization;
using System.Text.Json;
using ViesbucioPuslapis.Data;
using ViesbucioPuslapis.Models;

namespace ViesbucioPuslapis.Pages
{
    public class GymReservationViewModel : PageModel
    {
        private readonly ILogger<ErrorModel> _logger;
        private readonly HotelDbContext _db;

        [BindProperty]
        public int SessionId { get; set; }

        public int WeekDay { get; set; }
        public List<(TrainingSession, Trainer)> TrainingSess { get; set; }
        public GymReservationViewModel(ILogger<ErrorModel> logger, HotelDbContext db)
        {
            _logger = logger;
            _db = db;
        }
        public void OnGet()
        {
            string? start = Request.Query["Start"];
            string? end = Request.Query["End"];
            string? weekd = Request.Query["Weekd"];

            if (start == null || end == null || weekd == null) 
                return;

            TimeOnly startTime = TimeOnly.Parse(start);
            TimeOnly endTime = TimeOnly.Parse(end);
            WeekDay = int.Parse(weekd);

            CultureInfo myCI = new CultureInfo("lt-LT");
            Calendar myCal = myCI.Calendar;
            CalendarWeekRule myCWR = myCI.DateTimeFormat.CalendarWeekRule;
            DayOfWeek myFirstDOW = myCI.DateTimeFormat.FirstDayOfWeek;

            var rezervations = _db.sporto_sales_rezervacija;
            var training = _db.treniruote.ToList().Select(t => new TrainingSession
            {
                treniruotes_nr = t.treniruotes_nr,
                treniruotes_pradzia = t.treniruotes_pradzia,
                treniruotes_pabaiga = t.treniruotes_pabaiga,
                savaites_diena = t.savaites_diena,
                fk_Trenerisid_Treneris = t.fk_Trenerisid_Treneris,
                vietu_kiekis = t.vietu_kiekis - rezervations.ToList().Where(r =>
                    (r.fk_Treniruote_treniruotes_nr == t.treniruotes_nr) && (myCal.GetWeekOfYear(r.rezervacijos_laikas, myCWR, myFirstDOW).CompareTo(myCal.GetWeekOfYear(DateTime.Now, myCWR, myFirstDOW)) == 0)
                    ).Count(),
            }).ToList();


            var trainers =  _db.treneris.ToList();

            TrainingSess = training.Where(t=> t.treniruotes_pradzia == startTime && t.treniruotes_pabaiga==endTime && t.savaites_diena== WeekDay).Select(t=>(t, trainers.Where(tr=>t.fk_Trenerisid_Treneris==tr.id_Treneris).First())).ToList();


        }
        public IActionResult OnPostAddRezervation(int weekD)
        {
            TempData["SuccessRezervation"] = String.Format("Rezervacija sekmingai atlikta");
            _db.Add(new GymReservation { fk_Klientas_id_Naudotojas = 2, fk_Treniruote_treniruotes_nr = SessionId, rezervacijos_laikas = DateTime.Now});
            _db.SaveChanges();
            return Redirect($"/GymSystem/GymTimeList?WeekD={weekD}");
        }
    }
}
