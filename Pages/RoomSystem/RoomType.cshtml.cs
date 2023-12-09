using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor.Infrastructure;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ViesbucioPuslapis.Data;
using ViesbucioPuslapis.Models;

namespace ViesbucioPuslapis.Pages.RoomSystem
{
    public class RoomTypeModel : PageModel
    {
        private readonly ILogger<ErrorModel> _logger;
        private readonly HotelDbContext _db;
        public List<RoomTypes> roomTypes { get; set; }
        public string roomType;
        public int roomId { get; set; }
        public RoomTypeModel(ILogger<ErrorModel> logger, HotelDbContext db)
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
        }

    }
}
