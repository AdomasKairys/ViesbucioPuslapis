using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ViesbucioPuslapis.Data;
using ViesbucioPuslapis.Models;

namespace ViesbucioPuslapis.Pages.RoomSystem
{
    public class ReserveRoomModel : PageModel
    {
        private readonly ILogger<ErrorModel> _logger;
        private readonly HotelDbContext _db;

        public DateTime startDate;
        public DateTime endDate;
        public double reservationCost;

        public Room room { get; set; }
        public List<Reservation> reservations { get; set; }

        public ReserveRoomModel(ILogger<ErrorModel> logger, HotelDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public void OnGet(string roomnr, string start, string end)
        {
            if (start == null || end == null)
                return;

            startDate = DateTime.Parse(start);
            endDate = DateTime.Parse(end);

            TimeSpan timeSpent = endDate - startDate;
            double days = timeSpent.TotalDays;

            reservations = _db.kambario_rezervacija.ToList();
            room = _db.kambarys.First(r => r.kambario_numeris == roomnr.ToString());

            reservationCost = days * room.nakties_kaina;
        }
    }
}
