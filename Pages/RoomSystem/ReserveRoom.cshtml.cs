using Microsoft.AspNetCore.Mvc;
﻿using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ViesbucioPuslapis.Data;
using ViesbucioPuslapis.Models;

namespace ViesbucioPuslapis.Pages.RoomSystem
{
    public class ReserveRoomModel : PageModel
    {
        private readonly ILogger<ErrorModel> _logger;
        private readonly HotelDbContext _db;

        public DateTime startDate;
        public DateTime endDate;
        public double reservationCost;
        public string RoomNr { get; set; } = "";
        //public string RoomNr { get; set; } = "";

        public double days;

        public Room room { get; set; }
        public List<Reservation> reservations { get; set; }

        public ReserveRoomModel(ILogger<ErrorModel> logger, HotelDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public void OnGet(string roomnr, string start, string end)
        {
            if (start == null || end == null)
                return;

            startDate = DateTime.Parse(start);
            endDate = DateTime.Parse(end);

            TimeSpan timeSpent = endDate - startDate;
            days = timeSpent.TotalDays;

            reservations = _db.kambario_rezervacija.ToList();
            room = _db.kambarys.First(r => r.kambario_numeris == roomnr.ToString());

            reservationCost = days * room.nakties_kaina;

            RoomNr = roomnr.ToString();

            TempData["RoomNr"] = RoomNr;
            TempData["Days"] = days.ToString();
            TempData["Cost"] =  reservationCost.ToString();
        }

        public string errorMessage = "";

        public void OnPost()
        {
            string roomNrFromTempData = TempData["RoomNr"] as string;
            string daysFromTempData = TempData["Days"] as string;
            string costFromTempData = TempData["Cost"] as string;

            if (roomNrFromTempData != null && double.TryParse(daysFromTempData, out double days) && double.TryParse(costFromTempData, out double cost))
            {
                TempData["RoomNr"] = roomNrFromTempData;
                TempData["Days"] = daysFromTempData;
                TempData["Cost"] = costFromTempData;

                // Galite naudoti 'roomNrFromTempData', 'days' ir 'cost' reikšmes
                Response.Redirect("/ClientSystem/CheckoutPayment");
            }
            else
            {
                // Įvyksta klaida - galbūt nėra reikiamos informacijos 'TempData'
                errorMessage = "Failed";
            }
            
        }
        
        public IActionResult ReservationBtn_Click()
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
            var lastReservation = reservations.Last();
            int resId = lastReservation.id_Kambario_rezervacija + 1;

            _db.Add(new Reservation { id_Kambario_rezervacija = resId, pradžia = startDate, pabaiga = endDate, kaina = Decimal.Parse(reservationCost.ToString()), //this is so scuffed lol
                mokejimo_busena = paymentStatus, fk_Klientas_id_Naudotojas = tempID, fk_Kambarys_kambario_numeris = room.kambario_numeris});
            _db.SaveChanges();



            OnPost();
            //Neveikia redirectas :( bet iraso i db
            return Redirect("./OwnedReservation");
        }
    }
}
