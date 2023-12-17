using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ViesbucioPuslapis.Models
{
    public class TrainingSession
    {
        [Key]
        public int treniruotes_nr { get; set; }
        public TimeOnly treniruotes_pradzia { get; set; }
        public TimeOnly treniruotes_pabaiga { get; set; }
        public int vietu_kiekis { get; set; }
        public int savaites_diena { get; set; }

        public int fk_Trenerisid_Treneris { get; set; }

    }
}
