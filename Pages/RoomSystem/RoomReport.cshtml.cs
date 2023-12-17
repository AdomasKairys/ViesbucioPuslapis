using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;
using ViesbucioPuslapis.Data;
using ViesbucioPuslapis.Models;

namespace ViesbucioPuslapis.Pages.RoomSystem
{
    public class RoomReportModel : PageModel
    {
        private readonly ILogger<ErrorModel> _logger;
        private readonly HotelDbContext _db;
        public List<Reservation> Reservations { get; set; }
        public List<Room> Rooms { get; set; }
        public List<Room> MostPopularRooms { get; set; }
        public List<int> MostPopularCount { get; set; }
        public double AverageDaysStayed { get; set; }
        public decimal AverageCostPerStay { get; set; }

        public RoomReportModel(ILogger<ErrorModel> logger, HotelDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public void OnGet()
        {
            Reservations = _db.kambario_rezervacija.ToList();
            Rooms = _db.kambarys.ToList();

            //Top 3 most popular rooms
            var popularroomnr = Reservations
            .GroupBy(r => r.fk_Kambarys_kambario_numeris)
            .Select(g => new { RoomNumber = g.Key, ReservationCount = g.Count() })
            .OrderByDescending(rc => rc.ReservationCount)
            .Take(3)
            .ToList();

            MostPopularCount = popularroomnr.Select(r => r.ReservationCount).ToList();

            MostPopularRooms = new List<Room>();
            foreach (var room in popularroomnr)
            {
                var pop = Rooms
                    .Where(r => r.kambario_numeris == room.RoomNumber).ToList();

                MostPopularRooms.AddRange(pop);
            }

            //Average days stayed
            var daysstayed = Reservations
                .Select(r => r.pabaiga - r.pradžia).ToList();

            AverageDaysStayed = 0;
            foreach(var amount in daysstayed)
            {
                AverageDaysStayed += amount.TotalDays;

            }
            AverageDaysStayed /= Reservations.Count;

            //Average money spent per stay
            AverageCostPerStay = Reservations
                .Select(r => r.kaina).Average();


            //Most popular room type
        }
    }
}
