using System.ComponentModel.DataAnnotations;

namespace ViesbucioPuslapis.Models
{
    public class Reviews
    {
        [Key]
        public int id_Kambario_atsiliepimas { get; set; }
        public string komentaras { get; set; }
        public DateTime atsiliepimo_data { get; set; }
        public int fk_Kambario_rezervacija_id_Kambario_rezervacija { get; set; }
        public int fk_Klientas_id_Naudotojas { get;set; }
    }
}
