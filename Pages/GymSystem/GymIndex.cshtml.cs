using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using System.Globalization;
using ViesbucioPuslapis.Data;
using ViesbucioPuslapis.Models;

namespace ViesbucioPuslapis.Pages
{
    public class GymIndexModel : PageModel
    {
        private readonly ILogger<ErrorModel> _logger;
        private readonly HotelDbContext _db;
        public int RezervationCount { get; set; }
        public int FreeSlots{ get; set; }
        public int SessionAmm { get; set; }



        public GymIndexModel(ILogger<ErrorModel> logger, HotelDbContext db)
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
            }).Where(t => t.treniruotes_pradzia > TimeOnly.FromDateTime(DateTime.Now)).ToList();

            FreeSlots = training.Sum(t => t.vietu_kiekis);
            SessionAmm = training.Count();

            var trainer = _db.treneris.ToList();
            var userData = HttpContext.Session.GetComplexData<User>("user");
            if (userData == null)
                return;
            RezervationCount = rezervations.ToList().Select(r => (r, training.FirstOrDefault(t => r.fk_Treniruote_treniruotes_nr == t.treniruotes_nr)))
                .Select(r => (r.Item1, r.Item2, trainer.FirstOrDefault(t => r.Item2 != null ? r.Item2.fk_Trenerisid_Treneris == t.id_Treneris : false)))
                .Where(r => r.Item1.fk_Klientas_id_Naudotojas == userData.id_Naudotojas).Count();
        }
    }
}
