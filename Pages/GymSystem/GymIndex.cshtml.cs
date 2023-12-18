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
        public int[] RezervationCount { get; set; }
        public int[] FreeSlots{ get; set; }
        public int[] SessionAmm { get; set; }



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
            RezervationCount = new int[7];
            FreeSlots = new int[7];
            SessionAmm = new int[7];

            FreeSlots = Enumerable.Range(0, 7).Select( i => training.Where(t=>t.savaites_diena==i+1).Sum(t => t.vietu_kiekis)).ToArray();
            SessionAmm = Enumerable.Range(0, 7).Select(i => training.Where(t => t.savaites_diena == i + 1).Count()).ToArray();

            var trainer = _db.treneris.ToList();
            var userData = HttpContext.Session.GetComplexData<User>("user");
            if (userData == null)
                return;
            RezervationCount = Enumerable.Range(0, 7).Select(i => rezervations.ToList()
                    .Select(r => (r, training.FirstOrDefault(t => r.fk_Treniruote_treniruotes_nr == t.treniruotes_nr)))
                    .Select(r => (r.Item1, r.Item2, trainer.FirstOrDefault(t => r.Item2 != null ? r.Item2.fk_Trenerisid_Treneris == t.id_Treneris : false)))
                    .Where(r => r.Item1.fk_Klientas_id_Naudotojas == userData.id_Naudotojas && r.Item2.savaites_diena == i+1).Count()
                ).ToArray();
        }
    }
}
