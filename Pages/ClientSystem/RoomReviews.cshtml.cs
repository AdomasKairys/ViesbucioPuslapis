using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using System.Collections.Generic;

namespace ViesbucioPuslapis.Pages.ClientSystem
{
    public class Review
    {
        public string UserName { get; set; }
        public string Comment { get; set; }
    }
    public class RoomReviewModel : PageModel
    {
        public List<Review> Reviews { get; set; }

        public void OnGet()
        {
            // Čia gauti atsiliepimų duomenis iš duomenų bazės ar kitaip
            Reviews = GetReviewsFromDatabase();
        }

        private List<Review> GetReviewsFromDatabase()
        {
            // Čia galite įdėti kodą, kuris gautų kambario atsiliepimų duomenis iš duomenų bazės
            // Šiame pavyzdyje grąžinama kietoji duomenų struktūra
            return new List<Review>
            {
                new Review { UserName = "Jonas", Comment = "Puikus kambario įspūdis!" },
                new Review { UserName = "Petras", Comment = "Labai patogi lova." },
                // Pridėkite daugiau atsiliepimų pagal savo poreikius
            };
        }
    }
}
