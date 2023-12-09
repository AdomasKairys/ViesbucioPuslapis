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


        public List<Room> rooms { get; set; }
        public List<Reservation> reservations { get; set; }
        public List<Room> displayRooms { get; set; }


        public ReserveRoomListModel(ILogger<ErrorModel> logger, HotelDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public void OnGet(int roomid)
        {
            roomTypes = _db.kambario_tipas.ToList();
            RoomTypes temp = roomTypes.Single(type => type.id_Kambario_tipas == roomid);

            roomType = temp.name;
            roomId = temp.id_Kambario_tipas;

            rooms = _db.kambarys.ToList();

            //Filter room types
            foreach (var room in rooms.ToList())
            {
                if (room.tipas != roomid)
                {
                    rooms.Remove(room);
                }
            }

            displayRooms = rooms;
   
        }

        public IActionResult OnPost(int roomid, DateTime start, DateTime end)
        {
            if (reservations != null)
            {
                foreach (var res in reservations)
                {
                    foreach (var room in displayRooms.ToList())
                    {
                        if (res.pradžia >= start && res.pabaiga <= end && res.fk_Kambarys_kambario_numeris == room.kambario_numeris)
                        {
                            displayRooms.Remove(room);
                        }
                    }
                }
            }

            return RedirectToPage("");

        }

    }
}
