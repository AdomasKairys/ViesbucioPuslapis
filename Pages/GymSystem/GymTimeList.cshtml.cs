using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ViesbucioPuslapis.Data;
using ViesbucioPuslapis.Models;

namespace ViesbucioPuslapis.Pages
{
    public class GymTimeListModel : PageModel
    {
        private readonly ILogger<ErrorModel> _logger;
        private readonly HotelDbContext _db;
        public List<(TimeOnly, TimeOnly, int)> TrainingSess { get; set; }
        public GymTimeListModel(ILogger<ErrorModel> logger, HotelDbContext db)
        {
            _logger = logger;
            _db = db;
        }
        public void OnGet()
        {
            TrainingSess = _db.treniruote
                .GroupBy((sess) => new { sess.treniruotes_pradzia, sess.treniruotes_pabaiga }).AsEnumerable()
                .Select(g => (g.Key.treniruotes_pradzia, g.Key.treniruotes_pabaiga, g.Sum(s => s.vietu_kiekis))).ToList();
        }
    }
}
