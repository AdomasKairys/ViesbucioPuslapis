using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Globalization;
using ViesbucioPuslapis.Data;
using ViesbucioPuslapis.Models;

namespace ViesbucioPuslapis.Pages
{
    public class GymTimeListModel : PageModel
    {
        private readonly ILogger<ErrorModel> _logger;
        private readonly HotelDbContext _db;
        public List<(TimeOnly StartTime, TimeOnly EndTime, int SessId)> TrainingSess { get; set; }
        public List<(GymReservation Rezervation, TrainingSession TrainingSession, Trainer Trainer)> UserGymRezervation { get; set; }

        public int WeekD { get; set; }
        public GymTimeListModel(ILogger<ErrorModel> logger, HotelDbContext db)
        {
            _logger = logger;
            _db = db;
        }
        public void OnGet()
        {
            string? weekd = Request.Query["Weekd"];

            if(weekd == null)
                return;

            WeekD = int.Parse(weekd);

            CultureInfo myCI = new CultureInfo("lt-LT");
            Calendar myCal = myCI.Calendar;
            CalendarWeekRule myCWR = myCI.DateTimeFormat.CalendarWeekRule;
            DayOfWeek myFirstDOW = myCI.DateTimeFormat.FirstDayOfWeek;

            var rezervations = _db.sporto_sales_rezervacija;
            var training = _db.treniruote.ToList().Select(t=> new TrainingSession{
                treniruotes_nr = t.treniruotes_nr, 
                treniruotes_pradzia=t.treniruotes_pradzia,
                treniruotes_pabaiga=t.treniruotes_pabaiga,
                savaites_diena=t.savaites_diena, 
                fk_Trenerisid_Treneris=t.fk_Trenerisid_Treneris,
                vietu_kiekis=t.vietu_kiekis - rezervations.ToList().Where(r => 
                    (r.fk_Treniruote_treniruotes_nr == t.treniruotes_nr) && (myCal.GetWeekOfYear(r.rezervacijos_laikas, myCWR, myFirstDOW).CompareTo(myCal.GetWeekOfYear(DateTime.Now, myCWR, myFirstDOW)) == 0)
                    ).Count(),
            }).ToList();



            TrainingSess = training.Where(t=>t.savaites_diena==WeekD)
                .GroupBy((sess) => new { sess.treniruotes_pradzia, sess.treniruotes_pabaiga }).AsEnumerable()
                .Select(g => (g.Key.treniruotes_pradzia, g.Key.treniruotes_pabaiga, g.Sum(s => s.vietu_kiekis))).ToList();

            var trainer = _db.treneris.ToList();
            var userData = HttpContext.Session.GetComplexData<User>("user");
            if (userData == null)
                return;
            UserGymRezervation = rezervations.ToList().Select(r=> (r, training.First(t =>r.fk_Treniruote_treniruotes_nr==t.treniruotes_nr)))
                .Select(r=>(r.Item1, r.Item2, trainer.First(t =>r.Item2.fk_Trenerisid_Treneris == t.id_Treneris))) 
                .Where(r => r.Item1.fk_Klientas_id_Naudotojas == userData.id_Naudotojas).ToList();
        }
    }
}
