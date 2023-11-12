using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

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

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                // Hardcode'inti prisijungimo duomenys
                string hardcodedEmail = "stud@example.com";
                string hardcodedPassword = "stud";

                if (Input.Email == hardcodedEmail && Input.Password == hardcodedPassword)
                {
                    // Įvykdomas prisijungimas
                    TempData["SuccessMessage"] = "Sėkmingai prisijungta!";
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

