using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Globalization;
using ViesbucioPuslapis.Data;
using ViesbucioPuslapis.Models;

namespace ViesbucioPuslapis.Pages
{
    public class GymEditRezervationViewModel : PageModel
    {
        private readonly ILogger<ErrorModel> _logger;
        private readonly HotelDbContext _db;
        public GymReservation UserGymRezervation { get; set; }
        public Trainer UserTrainer { get; set; }
        public TrainingSession UserSession { get; set; }


        public List<(TrainingSession, Trainer)> TrainingSess { get; set; }

        public int Id { get; set; }

        public GymEditRezervationViewModel(ILogger<ErrorModel> logger, HotelDbContext db)
        {
            _logger = logger;
            _db = db;
        }
        public void OnGet()
        {
            string? id = Request.Query["id"];

            if (id == null)
                return;

            Id = int.Parse(id);

            UserGymRezervation = _db.sporto_sales_rezervacija.ToList().First(r => r.id_Sporto_sales_rezervacija == Id);
            UserSession = _db.treniruote.ToList().First(t => t.treniruotes_nr == UserGymRezervation.fk_Treniruote_treniruotes_nr);
            UserTrainer = _db.treneris.ToList().First(t => t.id_Treneris == UserSession.fk_Trenerisid_Treneris);

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
            }).Where(t => t.treniruotes_pradzia > TimeOnly.FromDateTime(DateTime.Now) || (t.savaites_diena == 7 ? 0 : t.savaites_diena) > myCal.GetWeekOfYear(DateTime.Now, myCWR, myFirstDOW)).ToList();

            var trainers = _db.treneris.ToList();

            TrainingSess = training.Where(t => t.savaites_diena == UserSession.savaites_diena).Select(t => (t, trainers.First(tr => t.fk_Trenerisid_Treneris == tr.id_Treneris))).ToList();

        }
        public IActionResult OnPost(int id)
        {
            OnGet();
            _db.Update(UserGymRezervation);

            UserGymRezervation.fk_Treniruote_treniruotes_nr = id;

            _db.SaveChanges();
            TempData["SuccessRezervation"] = "Rezervacija sėkmingai redaguota";

            return Redirect($"/GymSystem/GymTimeList?Weekd={UserSession.savaites_diena}");
        }
    }
}
