using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using ViesbucioPuslapis.Data;
using ViesbucioPuslapis.Models;

namespace ViesbucioPuslapis.Pages.ClientSystem
{
    public class ClientsListModel : PageModel
    {
        private readonly ILogger<ErrorModel> _logger;
        private readonly HotelDbContext _db;
        public List<Client> Clients { get; set; }

        public List<User> Users { get; set; }

        public ClientsListModel(ILogger<ErrorModel> logger, HotelDbContext db)
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

        /*public void OnGet()
        {
            // Gali būti imami klientų duomenys iš duomenų bazės arba kitos šaltinio
            // Ir užpildomas sąrašas
            Clients = GetClientsFromDatabase();
        }

        private List<Client> GetClientsFromDatabase()
        {
            // Čia galite įdėti kodą gauti klientų duomenis iš duomenų bazės
            // Šiame pavyzdyje grąžinama kietoji duomenų struktūra
            return new List<Client>
            {
                new Client { Id = 1, Name = "Jonas", Surname = "Jonaitis" },
                new Client { Id = 2, Name = "Petras", Surname = "Petraitis" },
                // Pridėkite daugiau klientų pagal savo poreikius
            };
        }*/
    }

}
