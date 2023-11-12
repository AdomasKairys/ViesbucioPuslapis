using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ViesbucioPuslapis.Pages.ClientSystem
{
    public class EditClientModel : PageModel
    {
        public Client Client { get; set; }

        public void OnGet(int id)
        {
            // Čia galite įdėti kodą gauti kliento duomenis pagal ID iš duomenų bazės
            // Šiame pavyzdyje grąžinama kietoji duomenų struktūra
            Client = GetClientById(id);
        }

        public IActionResult OnPost()
        {
            // Čia galite įdėti kodą, kuris atnaujina kliento duomenis duomenų bazėje
            // ir po to nukreipia vartotoją į klientų sąrašo puslapį
            // Po sėkmingo redagavimo, galite nukreipti vartotoją į kitą puslapį arba grįžti atgal į klientų sąrašą
            return RedirectToPage("./ClientsList");
        }

        private Client GetClientById(int id)
        {
            // Čia galite įdėti kodą gauti kliento duomenis pagal ID iš duomenų bazės
            // Šiame pavyzdyje grąžinama kietoji duomenų struktūra
            return new Client { Id = id, Name = "Jonas", Surname = "Jonaitis" };
        }
    }

}
