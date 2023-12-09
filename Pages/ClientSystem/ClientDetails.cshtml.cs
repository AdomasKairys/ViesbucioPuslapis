using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using ViesbucioPuslapis.Data;
using ViesbucioPuslapis.Models;

namespace ViesbucioPuslapis.Pages.ClientSystem
{
    public class ClientDetailsModel : PageModel
    {
        private readonly ILogger<ErrorModel> _logger;
        private readonly HotelDbContext _db;
        public List<Client> Clients { get; set; } = new List<Client>();
        public List<User> Users { get; set; } = new List<User>();

        public ClientDetailsModel(ILogger<ErrorModel> logger, HotelDbContext db)
        {
            _logger = logger;
            _db = db;
        }
        public void OnGet(int id)
        {
            //Clients = Clients.Where(client => client.id_Naudotojas == id).ToList();
            //Users = Users.Where(user => user.id_Naudotojas == id).ToList();
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

        //private Client GetClientById(int id)
        //{
        //    // Čia galite įdėti kodą gauti kliento duomenis pagal ID iš duomenų bazės
        //    // Šiame pavyzdyje grąžinama kietoji duomenų struktūra
        //    return new Client { Id = id, Name = "Jonas", Surname = "Jonaitis" };
        //}
    }

}
