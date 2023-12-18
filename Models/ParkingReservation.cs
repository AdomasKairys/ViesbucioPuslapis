using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ViesbucioPuslapis.Models
{
    public class ParkingReservation
    {
        [Key]
        public int id_Stovejimo_vietos_rezervacija { get; set; }
        public DateTime stovejimo_vietos_pradžia { get; set; }
        public DateTime stovejimo_vietos_pabaiga { get; set; }
        public int fk_Kambario_rezervacijaid_Kambario_rezervacija { get; set; }
        public string fk_Stovejimo_vietavietos_id { get; set; }
    }
}
