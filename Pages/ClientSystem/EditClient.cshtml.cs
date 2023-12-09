using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ViesbucioPuslapis.Data;
using ViesbucioPuslapis.Models;

namespace ViesbucioPuslapis.Pages.ClientSystem
{
    public class EditClientModel : PageModel
    {
        private readonly ILogger<ErrorModel> _logger;
        private readonly HotelDbContext _db;
        public List<Client> Clients { get; set; }
        public List<User> Users { get; set; }

        public EditClientModel(ILogger<ErrorModel> logger, HotelDbContext db)
        {
            _logger = logger;
            _db = db;
        }
        public void OnGet()
        {
            Clients = _db.klientas.ToList();
            Users = _db.naudotojas.ToList();
            //string con = "server=localhost;user=adokai;Database=viesbucio_sistema;password=146025123";
            //MySqlConnection mySqlConnection = new MySqlConnection(con);
            //mySqlConnection.Open();
        }
        //public Client Client { get; set; }

        //public void OnGet(int id)
        //{
        //    // Čia galite įdėti kodą gauti kliento duomenis pagal ID iš duomenų bazės
        //    // Šiame pavyzdyje grąžinama kietoji duomenų struktūra
        //    Client = GetClientById(id);
        //}

        //public IActionResult OnPost()
        //{
        //    // Čia galite įdėti kodą, kuris atnaujina kliento duomenis duomenų bazėje
        //    // ir po to nukreipia vartotoją į klientų sąrašo puslapį
        //    // Po sėkmingo redagavimo, galite nukreipti vartotoją į kitą puslapį arba grįžti atgal į klientų sąrašą
        //    return RedirectToPage("./ClientsList");
        //}

        //private Client GetClientById(int id)
        //{
        //    // Čia galite įdėti kodą gauti kliento duomenis pagal ID iš duomenų bazės
        //    // Šiame pavyzdyje grąžinama kietoji duomenų struktūra
        //    return new Client { Id = id, Name = "Jonas", Surname = "Jonaitis" };
        //}
    }

}
