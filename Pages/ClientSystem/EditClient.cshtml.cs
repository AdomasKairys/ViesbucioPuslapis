using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using ViesbucioPuslapis.Data;
using ViesbucioPuslapis.Models;

namespace ViesbucioPuslapis.Pages.ClientSystem
{
    public class EditClientModel : PageModel
    {
        private readonly ILogger<EditClientModel> _logger;
        private readonly HotelDbContext _db;

        [BindProperty]
        public Client Client { get; set; }

        [BindProperty]
        public User User { get; set; }

        public EditClientModel(ILogger<EditClientModel> logger, HotelDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult OnGet(int id)
        {
            // Gauti kliento ir naudotojo duomenis iš duomenų bazės
            Client = _db.klientas.FirstOrDefault(c => c.id_Naudotojas == id);
            User = _db.naudotojas.FirstOrDefault(u => u.id_Naudotojas == id);

            Console.WriteLine("Client: " + Client?.ToString());
            Console.WriteLine("User: " + User?.ToString());
            if (Client == null || User == null)
            {
                return NotFound(); // Grąžinti 404, jei kliento ar naudotojo nerasta
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            //if (!ModelState.IsValid)
            //{
            //    // Log or print ModelState errors
            //    foreach (var modelState in ModelState.Values)
            //    {
            //        foreach (var error in modelState.Errors)
            //        {
            //            Console.WriteLine(error.ErrorMessage);
            //        }
            //    }
            //    return Page();
            //}
            foreach (var modelState in ModelState.Values)
            {
                foreach (var error in modelState.Errors)
                {
                    Console.WriteLine(error.ErrorMessage);
                }
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
            }

            _db.SaveChanges();
            //_db.Update(existingUser);
            //_db.Update(existingClient);

            // Naujas kodas: Nukreipkite į klientų sąrašo puslapį po sėkmingo išsaugojimo
            return RedirectToPage("/ClientSystem/ClientsList");
        }

    }
}
