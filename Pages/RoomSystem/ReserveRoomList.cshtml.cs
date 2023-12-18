using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ViesbucioPuslapis.Data;
using ViesbucioPuslapis.Models;

namespace ViesbucioPuslapis.Pages.RoomSystem
{
    public class ReserveRoomListModel : PageModel
    {
        private readonly ILogger<ErrorModel> _logger;
        private readonly HotelDbContext _db;

        public List<RoomTypes> roomTypes { get; set; }
        public string roomType;
        public int roomId { get; set; }

        public DateTime startDate;
        public DateTime endDate;


        public List<Room> rooms { get; set; }
        public List<Reservation> reservations { get; set; }


        public ReserveRoomListModel(ILogger<ErrorModel> logger, HotelDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public void OnGet(int roomid, string start, string end)
        {
            if (start == null || end == null)
                return;

            startDate = DateTime.Parse(start);
            endDate = DateTime.Parse(end);

            roomTypes = _db.kambario_tipas.ToList();
            RoomTypes temp = roomTypes.Single(type => type.id_Kambario_tipas == roomid);

            roomType = temp.name;
            roomId = temp.id_Kambario_tipas;

            reservations = _db.kambario_rezervacija.Where(res => res.pradþia >= startDate && res.pabaiga <= endDate).ToList();
            rooms = _db.kambarys.Where(room => room.tipas == roomId).ToList();

            foreach (var room in rooms.ToList())
            {
                foreach(var res in reservations)
                {
                    if (res.fk_Kambarys_kambario_numeris == room.kambario_numeris)
                    {
                        rooms.Remove(room);
                    }
                }

            }
             
        }

        public IActionResult OnPost()
        {
            string roomnr = Request.Form["roomnr"];
            string start = Request.Form["start"];
            string end = Request.Form["end"];

            return Redirect(string.Format("./ReserveRoom?roomnr={0}&start={1}&end={2}", roomnr, start, end));
        }

    }
}
