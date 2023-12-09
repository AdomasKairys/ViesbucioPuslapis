using System.ComponentModel.DataAnnotations;

namespace ViesbucioPuslapis.Models
{
    public class Reservation
    {
        [Key]
        public int id_Kambario_rezervacija { get; set; }
        public DateTime pradžia { get; set; }
        public DateTime pabaiga { get; set; }
        public decimal kaina { get; set; }
        public int mokejimo_busena { get; set; } //foreign key
        public int fk_Klientas_id_Naudotojas { get; set; }
        public string fk_Kambarys_kambario_numeris { get; set; }
    }
}
