using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

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

        }

        public IActionResult OnPost()
        {

            if (Input.Password1 == Input.Password2)
            {
                // Įvykdomas prisijungimas
                return RedirectToPage("/Index"); // Pridėkite puslapio kelio pavadinimą, į kurį nukreipiama po sėkmingo prisijungimo
            }
            else
            {
                // Klaida - neteisingi prisijungimo duomenys
                ModelState.AddModelError(string.Empty, "Nesutampa slaptažodis.");
                return Page();
            }
        }
    }
}
