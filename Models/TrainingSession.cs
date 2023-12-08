using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ViesbucioPuslapis.Models
{
    public class TrainingSession
    {
        [Key]
        public int treniruotes_nr { get; set; }
        public DateTime treniruotes_pradzia { get; set; }
        public DateTime treniruotes_pabaiga { get; set; }
        public int vietu_kiekis { get; set; }
        public int fk_Trenerisid_Treneris { get; set; }

    }
}
