using Microsoft.AspNetCore.Mvc.RazorPages;
using System;

namespace ViesbucioPuslapis.Pages.ClientSystem
{
    public class SubmitPaymentModel : PageModel
    {
        public int RoomNumber { get; set; }
        public DateTime ReservationDate { get; set; }

        public void OnGet(int roomNumber, DateTime reservationDate)
        {
            // Šis metodas yra iškviečiamas, kai puslapis įkeliamas.
            RoomNumber = roomNumber;
            ReservationDate = reservationDate;
        }
    }
}
