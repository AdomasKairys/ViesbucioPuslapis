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
            [Required]
            public string FirstName { get; set; }

            [Required]
            public string LastName { get; set; }

            [Required]
            [Phone]
            public string PhoneNumber { get; set; }

            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password1 { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password2 { get; set; }

            [Required]
            public DateOnly BirthDate { get; set; }

            [Required]
            public string Gender { get; set; }
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
            bool error = false;
            if (Input.Password1 != Input.Password2)
            {
                ModelState.AddModelError(string.Empty, "Nesutampa slaptažodis.");
                error = true;
            }
            if (emailCheck)
            {
                ModelState.AddModelError(string.Empty, "Vartotojas su tokiu el. paštu jau egzistuoja.");
                error = true;
            }
            if(error)
                return Page();


            string salt = BCrypt.Net.BCrypt.GenerateSalt();
            string passHash = BCrypt.Net.BCrypt.HashPassword(Input.Password1, salt);

            var user = new User { elektroninis_paštas = Input.Email, naudotojo_vardas = Input.FirstName, naudotojo_pavarde = Input.LastName, slaptažodis = passHash };

            _db.Add(user);
            _db.SaveChanges();

            var client = new Client { id_Naudotojas = user.id_Naudotojas, kliento_telefono_numeris = Input.PhoneNumber, kliento_gimimo_data = Input.BirthDate, kliento_lytis = Input.Gender };

            _db.Add(client);
            _db.SaveChanges();

            TempData["SuccessMessage"] = "Registracija sekminga!";
            return RedirectToPage("/Index");
        }
    }
}
