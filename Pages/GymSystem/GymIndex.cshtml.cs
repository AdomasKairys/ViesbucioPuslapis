using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using ViesbucioPuslapis.Data;
using ViesbucioPuslapis.Models;

namespace ViesbucioPuslapis.Pages
{
    public class GymIndexModel : PageModel
    {
        private readonly ILogger<ErrorModel> _logger;
        private readonly HotelDbContext _db;
        public List<TrainingSession> TrainingSess { get; set; }
        public List<GymReservation> GymReservation { get; set; }

        public GymIndexModel(ILogger<ErrorModel> logger, HotelDbContext db)
        {
            _logger = logger;
            _db = db;
        }
        public void OnGet()
        {
            TrainingSess = _db.treniruote.ToList();
            GymReservation = _db.sporto_sales_rezervacija.ToList();




            //string con = "server=localhost;user=adokai;Database=viesbucio_sistema;password=146025123";
            //MySqlConnection mySqlConnection = new MySqlConnection(con);
            //mySqlConnection.Open();
        }
    }
}
