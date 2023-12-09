using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ViesbucioPuslapis.Data;
using ViesbucioPuslapis.Models;

namespace ViesbucioPuslapis.Pages
{
    public class GymReservationViewModel : PageModel
    {
        private readonly ILogger<ErrorModel> _logger;
        private readonly HotelDbContext _db;
        public List<TrainingSession> TrainingSess { get; set; }
        public GymReservationViewModel(ILogger<ErrorModel> logger, HotelDbContext db)
        {
            _logger = logger;
            _db = db;
        }
        public void OnGet()
        {
            string? start = Request.Query["Start"];
            string? end = Request.Query["End"];

            if(start == null || end == null) 
                return;

            TimeOnly startTime = TimeOnly.Parse(start);
            TimeOnly endTime = TimeOnly.Parse(end);

            TrainingSess = _db.treniruote.Where(t=> t.treniruotes_pradzia == startTime && t.treniruotes_pabaiga==endTime).ToList();

        }
        public IActionResult OnPost()
        {
            string? trainingSessId = Request.Form["TsessID"];
            if (trainingSessId != null && int.TryParse(trainingSessId, out int id))
            {
                Console.WriteLine("a");
                _db.Add(new GymReservation { fk_Klientas_id_Naudotojas = 2, fk_Treniruote_treniruotes_nr = id, rezervacijos_laikas = DateTime.Now});
                _db.SaveChanges();
            }
            return Redirect("./GymTimeList");
        }
    }
}
