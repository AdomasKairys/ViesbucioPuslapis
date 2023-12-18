using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ViesbucioPuslapis.Models;
using ViesbucioPuslapis.Data;


namespace ViesbucioPuslapis.Pages.ClientSystem
{
    public class LeaveRoomReviewModel : PageModel
    {
        private readonly ILogger<ErrorModel> _logger;
        private readonly HotelDbContext _db;

        public List<Reviews> reviews { get; set; }
        

        public LeaveRoomReviewModel(ILogger<ErrorModel> logger, HotelDbContext db)
        {
            _logger = logger;
            _db = db;
        }
        public void OnGet()
        {
        }
        public IActionResult ReviewBtn_Click()
        {
            //check if payment is done
            bool paymentSuccess = true; //Changes depending on payment
            int paymentStatus;

            int tempID = 2; //tempID - clientid; need a method to fetch value

            //Mokėjimo būsenos: 1- Apmokėta; 2-Neapmokėta; 3-Atšaukta
            if (paymentSuccess)
                paymentStatus = 1;
            else
                paymentStatus = 2;

            //reservationid calculation
            var lastReview = reviews.Last();
            int revId = lastReview.id_Kambario_atsiliepimas + 1;
            //Review review = new Review();
            
            _db.Add(new Reviews
            {
                id_Kambario_atsiliepimas = revId,
                komentaras = "",
                atsiliepimo_data = DateTime.Now,
                fk_Kambario_rezervacija_id_Kambario_rezervacija = tempID,
                fk_Klientas_id_Naudotojas = tempID
            });
            _db.SaveChanges();

            
            return Redirect("./OwnedReservation");
        }
    }
}