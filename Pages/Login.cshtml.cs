using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BCrypt.Net;
using ViesbucioPuslapis.Data;
using ViesbucioPuslapis.Models;

namespace ViesbucioPuslapis.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }
        private readonly ILogger<ErrorModel> _logger;
        private readonly HotelDbContext _db;
        public List<User> users { get; set; }

        public LoginModel(ILogger<ErrorModel> logger, HotelDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                User? user;
                if ((user =_db.naudotojas.Where(user => user.elektroninis_paštas == Input.Email).FirstOrDefault()) != null && 
                    BCrypt.Net.BCrypt.Verify(Input.Password, user.slaptažodis))
                {
                    // Įvykdomas prisijungimas
                    TempData["SuccessMessage"] = String.Format("Sėkmingai prisijungta! Prisijungimo pastas: {0}", Input.Email);

                    return RedirectToPage("/Index");
                }
                else
                {
                    // Klaida - neteisingi prisijungimo duomenys
                    ModelState.AddModelError(string.Empty, "Neteisingas prisijungimo bandymas.");
                    return Page();
                }
            }
            else
            {
                // Klaida - ne visi reikiami duomenys pateikti
                ModelState.AddModelError(string.Empty, "Užpildykite visus laukus.");
                return Page();
            }
        }

    }
}

