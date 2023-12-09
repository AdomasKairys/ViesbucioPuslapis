using System.ComponentModel.DataAnnotations;
using BCrypt.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ViesbucioPuslapis.Data;
using ViesbucioPuslapis.Models;

namespace ViesbucioPuslapis.Pages
{
    public class RegisterModel : PageModel
    {
        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "Vardas yra privalomas")]
            public string FirstName { get; set; }

            [Required(ErrorMessage = "Pavardė yra privaloma")]
            public string LastName { get; set; }

            [Required(ErrorMessage = "Tel. numeris yra privalomas")]
            [Phone(ErrorMessage = "Blogas tel. numerio formatas")]
            public string PhoneNumber { get; set; }

            [Required(ErrorMessage = "El. pašto adresas yra privalomas")]
            [EmailAddress(ErrorMessage ="Blogas el. pašto formatas")]
            public string Email { get; set; }

            [Required(ErrorMessage = "Slaptažodis yra privalomas")]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Required(ErrorMessage = "Prašome pakartoti slaptažodi")]
            [DataType(DataType.Password)]
            [Compare(nameof(Password), ErrorMessage = "Slaptažodis nesutampa")]
            public string Password2 { get; set; }

        }

        private readonly ILogger<ErrorModel> _logger;
        private readonly HotelDbContext _db;
        public List<User> users { get; set; }

        public RegisterModel(ILogger<ErrorModel> logger, HotelDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult OnPost()
        {
            var emailCheck = _db.naudotojas.Any(user => user.elektroninis_paštas == Input.Email);
            if (emailCheck)
            {
                ModelState.AddModelError($"{nameof(Input)}.{nameof(Input.Email)}", "Vartotojas su tokiu el. paštu jau egzistuoja.");
            }
            if (!ModelState.IsValid)
            {
                return Page();
            }


            string salt = BCrypt.Net.BCrypt.GenerateSalt();
            string passHash = BCrypt.Net.BCrypt.HashPassword(Input.Password, salt);

            var user = new User { elektroninis_paštas = Input.Email, naudotojo_vardas = Input.FirstName, naudotojo_pavarde = Input.LastName, slaptažodis = passHash };

            _db.Add(user);
            _db.SaveChanges();

            var client = new Client { id_Naudotojas = user.id_Naudotojas, kliento_telefono_numeris = Input.PhoneNumber };

            _db.Add(client);
            _db.SaveChanges();

            TempData["SuccessMessage"] = "Registracija sekminga!";
            return RedirectToPage("/Index");
        }
    }
}
