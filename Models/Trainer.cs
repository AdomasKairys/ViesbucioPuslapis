using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ViesbucioPuslapis.Models
{
    public class Trainer
    {
        [Key]
        public int id_Treneris { get; set; }
        public string trenerio_vardas { get; set; }
        public string trenerio_pavarde { get; set; }
        public string trenerio_telefono_numeris { get; set; }
        public string specifikacija { get; set; }

    }
}
