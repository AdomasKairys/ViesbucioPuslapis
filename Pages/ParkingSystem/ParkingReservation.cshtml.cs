using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ViesbucioPuslapis.Data;
using ViesbucioPuslapis.Models;

namespace ViesbucioPuslapis.Pages
{
    public class ParkingReservationModel : PageModel
    {
        private readonly ILogger<ParkingReservationModel> _logger;
        private readonly HotelDbContext _db;

        [BindProperty]
        public DateTime startDate { get; set; }
        [BindProperty]
        public DateTime endDate { get; set; }
        public Reservation reservation { get; set; }
        public List<Reservation> reservations { get; set; }
        public List<ParkingPlace> places { get; set; }
        [BindProperty]
        public int SelectedReservationId { get; set; }
        [BindProperty]
        public string SelectedPlaceId { get; set; }


        public ParkingReservationModel(ILogger<ParkingReservationModel> logger, HotelDbContext db)
        {
            _logger = logger;
            _db = db;
        }
        public void OnGet()
        {
            var user = HttpContext.Session.GetComplexData<User>("user");

            if (user == null)
            {
                return;
            }

            int userid = user.id_Naudotojas;

            reservations = _db.kambario_rezervacija.Where(res => res.fk_Klientas_id_Naudotojas == userid).ToList();
            places = _db.stovejimo_vieta.Where(res => res.vietos_užimtumas == false).ToList();

        }

        public IActionResult OnPost(string type)
        {
            if(type == "1")
            {
                ParkingReservation parkingReservation;

                //parkingReservation = _db.stovejimo_vietos_rezervacija.Where(res => res.fk_Kambario_rezervacijaid_Kambario_rezervacija == SelectedReservationId).First();
                //if(parkingReservation != null)
                //{
                //    return Redirect("/");
                //}

                parkingReservation= new ParkingReservation();
                ParkingPlace parking = _db.stovejimo_vieta.Where(res => res.vietos_id == SelectedPlaceId).FirstOrDefault();

                if(startDate == DateTime.MinValue || endDate == DateTime.MinValue)
                    return Redirect("/");

                parkingReservation.stovejimo_vietos_pradžia = startDate;
                parkingReservation.stovejimo_vietos_pabaiga = endDate;
                parkingReservation.fk_Stovejimo_vietavietos_id = SelectedPlaceId;
                parkingReservation.fk_Kambario_rezervacijaid_Kambario_rezervacija = SelectedReservationId;
                parking.užimta_nuo = startDate;
                parking.užimta_iki = endDate;
                parking.vietos_užimtumas = true;

                _db.Add(parkingReservation);
                _db.Update(parking);
                _db.SaveChanges();
            }
            else
            {
                ParkingReservation parkingReservation;

                //parkingReservation = _db.stovejimo_vietos_rezervacija.Where(res => res.fk_Kambario_rezervacijaid_Kambario_rezervacija == SelectedReservationId).First();
                //if (parkingReservation != null)
                //{
                //    return Redirect("/");
                //}

                parkingReservation = new ParkingReservation();
                ParkingPlace parking = _db.stovejimo_vieta.Where(res => res.vietos_id == SelectedPlaceId).FirstOrDefault();
                Reservation reservation = _db.kambario_rezervacija.Where(res => res.id_Kambario_rezervacija == SelectedReservationId).FirstOrDefault();
                ParkingPlace toBeSelected = _db.stovejimo_vieta.Where(res => res.vietos_užimtumas == false && res.aukstas_id == int.Parse(reservation.fk_Kambarys_kambario_numeris.FirstOrDefault().ToString())).OrderBy(res => res.vietos_id).FirstOrDefault();
                Console.WriteLine(int.Parse(reservation.fk_Kambarys_kambario_numeris.FirstOrDefault().ToString()));
                if(toBeSelected == null)
                {
                    toBeSelected = _db.stovejimo_vieta.Where(res => res.vietos_užimtumas == false).OrderBy(res => res.vietos_id).FirstOrDefault();
                }

                parkingReservation.stovejimo_vietos_pradžia = reservation.pradžia;
                parkingReservation.stovejimo_vietos_pabaiga = reservation.pabaiga;
                parkingReservation.fk_Stovejimo_vietavietos_id = toBeSelected.vietos_id;
                parkingReservation.fk_Kambario_rezervacijaid_Kambario_rezervacija = SelectedReservationId;
                parking.užimta_nuo = reservation.pradžia;
                parking.užimta_iki = reservation.pabaiga;
                parking.vietos_užimtumas = true;

                _db.Add(parkingReservation);
                _db.Update(parking);
                _db.SaveChanges();

                //for(int i = 0; i < 300; i++)
                //{
                //    ParkingPlace parking = new ParkingPlace();
                //    if(i < 100)
                //    {
                //        parking.aukstas_id = 1;
                //        parking.vietos_užimtumas = false;
                //        parking.užimta_iki = DateTime.MinValue;
                //        parking.užimta_nuo = DateTime.MinValue;
                //        parking.vietos_id = (i + 100).ToString();
                //    }
                //    else if(i < 200)
                //    {
                //        parking.aukstas_id = 2;
                //        parking.vietos_užimtumas = false;
                //        parking.užimta_iki = DateTime.MinValue;
                //        parking.užimta_nuo = DateTime.MinValue;
                //        parking.vietos_id = (i + 100).ToString();
                //    }
                //    else
                //    {
                //        parking.aukstas_id = 3;
                //        parking.vietos_užimtumas = false;
                //        parking.užimta_iki = DateTime.MinValue;
                //        parking.užimta_nuo = DateTime.MinValue;
                //        parking.vietos_id = (i + 100).ToString();
                //    }
                //    _db.Add(parking);
                //    _db.SaveChanges();
                //}
            }
            return Redirect("./ParkingReservationView");
        }
    }
}
