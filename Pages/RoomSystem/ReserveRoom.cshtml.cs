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

            TempData["start"] = start;
            TempData["end"] = end;
            TempData["RoomNr"] = RoomNr;
            TempData["Days"] = days.ToString();
            TempData["Cost"] =  reservationCost.ToString();
        }

        public string errorMessage = "";

        public IActionResult ReservationBtn_Click()
        {
            string roomNrFromTempData = TempData["RoomNr"] as string;
            string daysFromTempData = TempData["Days"] as string;
            string costFromTempData = TempData["Cost"] as string;

            if (roomNrFromTempData != null && double.TryParse(daysFromTempData, out double days) && double.TryParse(costFromTempData, out double cost))
            {
                TempData["RoomNr"] = roomNrFromTempData;
                TempData["Days"] = daysFromTempData;
                TempData["Cost"] = costFromTempData;

                Response.Redirect("/ClientSystem/CheckoutPayment");
            }
            else
            {
                // Įvyksta klaida - galbūt nėra reikiamos informacijos 'TempData'
                errorMessage = "Failed";
            }

            return Redirect("/Index");
        }
    }

}
