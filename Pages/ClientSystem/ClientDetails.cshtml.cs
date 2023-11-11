using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ViesbucioPuslapis.Pages.ClientSystem
{
    public class ClientDetailsModel : PageModel
    {
        public Client Client { get; set; }

        public void OnGet(int id)
        {
            // Čia galite įdėti kodą gauti kliento duomenis pagal ID iš duomenų bazės
            // Šiame pavyzdyje grąžinama kietoji duomenų struktūra
            Client = GetClientById(id);
        }

        private Client GetClientById(int id)
        {
            // Čia galite įdėti kodą gauti kliento duomenis pagal ID iš duomenų bazės
            // Šiame pavyzdyje grąžinama kietoji duomenų struktūra
            return new Client { Id = id, Name = "Jonas", Surname = "Jonaitis" };
        }
    }

}
