using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BCrypt.Net;
using ViesbucioPuslapis.Data;
using ViesbucioPuslapis.Models;
using System.Text.Json;

namespace ViesbucioPuslapis.Pages
{
    public static class SessionExtensions
    {
        public static JsonSerializerOptions options = new JsonSerializerOptions { IncludeFields = true };
        public static T GetComplexData<T>(this ISession session, string key)
        {
            var data = session.GetString(key);
            if (data == null)
            {
                return default(T);
            }
            return JsonSerializer.Deserialize<T>(data, options);
        }

        public static void SetComplexData(this ISession session, string key, object value)
        {
            session.SetString(key, JsonSerializer.Serialize(value, options));
        }
    }
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
                    //HttpContext.Session.SetString("UserEmail", user.elektroninis_paštas);


                    // Įvykdomas prisijungimas
                    TempData["SuccessMessage"] = String.Format("Sėkmingai prisijungta! Prisijungimo pastas: {0}", Input.Email);

                    HttpContext.Session.SetComplexData("user", user);
                    var admin = _db.administratorius.ToList().FirstOrDefault(a => a.id_Naudotojas == user.id_Naudotojas);

                    if(admin != null)
                    {
                        HttpContext.Session.SetComplexData("admin", admin);
                    }

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

