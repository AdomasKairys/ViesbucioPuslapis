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
        public class Specialization
        {
            public double kulturizmas { get; set; }
            public double jegos_trikove { get; set; }
            public double legvoji_atletika { get; set; }
            public double sunkioji_atletika { get; set; }
        }


        private readonly ILogger<ErrorModel> _logger;
        private readonly HotelDbContext _db;

        [BindProperty]
        public int SessionId { get; set; }
        [BindProperty]
        public Specialization UserSpec { get; set; }
        public int WeekDay { get; set; }
        public List<(TrainingSession Session, Trainer Train, Specialization Spec)> AutoSelected { get; set; }

        public List<(TrainingSession, Trainer)> TrainingSess { get; set; }
        public GymReservationViewModel(ILogger<ErrorModel> logger, HotelDbContext db)
        {
            _logger = logger;
            _db = db;
        }
        public (Trainer Train, Specialization Spec)? AutomaticSelect(List<Trainer> trainers)
        {
            List<(Trainer Train, Specialization Spec)> specs = trainers.Select(t => (t, JsonSerializer.Deserialize<Specialization>(t.specifikacija))).ToList();

            (Trainer Train, Specialization Spec)? returnVal = null;
            double score = double.MaxValue;
            foreach (var spec in specs)
            {
                double curr = 0;
                foreach (var prop in spec.Spec.GetType().GetProperties())
                {
                    curr += Math.Pow((double)prop.GetValue(spec.Spec)-(double)prop.GetValue(UserSpec), 2);
                }
                curr = Math.Sqrt(curr);
                if(score > curr)
                {
                    score = curr;
                    returnVal = spec;
                }
            }
            return returnVal;

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
            }).Where(t => t.treniruotes_pradzia > TimeOnly.FromDateTime(DateTime.Now) || (t.savaites_diena == 7 ? 0 : t.savaites_diena) > myCal.GetWeekOfYear(DateTime.Now, myCWR, myFirstDOW)).ToList();

            var trainers =  _db.treneris.ToList();

            TrainingSess = training.Where(t=> t.treniruotes_pradzia == startTime && t.treniruotes_pabaiga == endTime && t.savaites_diena == WeekDay).Select(t=>(t, trainers.First(tr=>t.fk_Trenerisid_Treneris==tr.id_Treneris))).ToList();
        }
        public IActionResult OnPostAddRezervation(int weekD)
        {
            var userData = HttpContext.Session.GetComplexData<User>("user");
            var clientId = _db.klientas.ToList().Where(c => c.id_Naudotojas == userData.id_Naudotojas).FirstOrDefault();
            if(clientId == null)
            {
                return Redirect("/Index");
            }
            TempData["SuccessRezervation"] = String.Format("Rezervacija sekmingai atlikta");
            _db.Add(new GymReservation { fk_Klientas_id_Naudotojas = clientId.id_Naudotojas, fk_Treniruote_treniruotes_nr = SessionId, rezervacijos_laikas = DateTime.Now});
            _db.SaveChanges();
            return Redirect($"/GymSystem/GymTimeList?WeekD={weekD}");
        }
        public IActionResult OnPost()
        {
            OnGet();
            (Trainer Train, Specialization Spec)? auto = AutomaticSelect(TrainingSess.Select(t => t.Item2).ToList());

            if(auto == null)
                return Page();

            (Trainer Train, Specialization Spec) = ((Trainer Train, Specialization Spec))auto;

            AutoSelected = TrainingSess.Select(t => (t.Item1, t.Item2, Spec)).Where(t => t.Item2.id_Treneris == Train.id_Treneris).ToList();

            return Page();
        }
    }
}
