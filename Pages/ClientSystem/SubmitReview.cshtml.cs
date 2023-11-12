using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ViesbucioPuslapis.Pages.ClientSystem
{
    public class SubmitReviewModel : PageModel
    {
        public bool IsReviewSubmitted { get; set; }

        public void OnGet()
        {
            // Šis metodas yra iškviečiamas, kai puslapis įkeliamas.
        }

        public IActionResult OnPost()
        {
            // Čia jūs turėtumėte apdoroti atsiliepimo pateikimą, išsaugoti į duomenų bazę ar kitoje vietoje.

            // Pavyzdinis kodas:
             IsReviewSubmitted = true;

            // Grįžti į puslapį su sėkmingo atsiliepimo pranešimu.
            return Page();
        }
    }
}
