using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ViesbucioPuslapis.Models
{
    public class GymRezervation
    {
        public int id_Sporto_sales_rezervacija { get; set; }
        public DateTime laiko_pradzia { get; set; }
        public  DateTime laiko_pabaiga { get; set; }
        public int fk_Klientas_id_Naudotojas { get; set; }
        public int fk_Treniruote_treniruotes_nr { get; set; }
    }
}
