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

        [BindProperty]
        public Client Client { get; set; } // Pridėkite šią eilutę

        [BindProperty]
        public User User { get; set; } // Pridėkite šią eilutę

        public EditClientModel(ILogger<ErrorModel> logger, HotelDbContext db)
        {
            _logger = logger;
            _db = db;
        }
        public void OnGet(int id)
        {
            Client = _db.klientas.FirstOrDefault(c => c.id_Naudotojas == id);
            User = _db.naudotojas.FirstOrDefault(c => c.id_Naudotojas == id);

            Console.WriteLine("Client: " + Client?.ToString());
            Console.WriteLine("User: " + User?.ToString());
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                // Log or print ModelState errors
                foreach (var modelState in ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        Console.WriteLine(error.ErrorMessage);
                    }
                }
                return Page();
            }


            // Atnaujinkite duomenis duomenų bazėje
            var existingUser = _db.naudotojas.FirstOrDefault(u => u.id_Naudotojas == User.id_Naudotojas);
            var existingClient = _db.klientas.FirstOrDefault(c => c.id_Naudotojas == Client.id_Naudotojas);

            if (existingUser != null && existingClient != null)
            {
                // Atnaujinkite duomenis iš formos
                existingUser.naudotojo_vardas = User.naudotojo_vardas;
                existingUser.naudotojo_pavarde = User.naudotojo_pavarde;
                existingUser.elektroninis_paštas = User.elektroninis_paštas;
                existingUser.slaptažodis = User.slaptažodis;

                existingClient.kliento_telefono_numeris = Client.kliento_telefono_numeris;
                existingClient.kliento_gimimo_data = Client.kliento_gimimo_data;


                try
                {
                    _db.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error saving changes: " + ex.Message);
                    // Log the exception or handle it appropriately
                }

            }

            return RedirectToPage("./ClientList"); // Nukreipkite į klientų sąrašo puslapį po sėkmingo išsaugojimo
        }

    }

}
