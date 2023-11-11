using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace ViesbucioPuslapis.Pages.ClientSystem
{
    public class ClientsListModel : PageModel
    {
        public List<Client> Clients { get; set; }

        public void OnGet()
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
        }
    }

    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
