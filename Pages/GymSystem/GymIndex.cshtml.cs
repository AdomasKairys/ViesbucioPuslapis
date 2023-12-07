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
        public List<Trainer> Trainers { get; set; }

        public GymIndexModel(ILogger<ErrorModel> logger, HotelDbContext db)
        {
            _logger = logger;
            _db = db;
        }
        public void OnGet()
        {
            Trainers = _db.treneris.ToList();
            //string con = "server=localhost;user=adokai;Database=viesbucio_sistema;password=146025123";
            //MySqlConnection mySqlConnection = new MySqlConnection(con);
            //mySqlConnection.Open();
        }
    }
}
