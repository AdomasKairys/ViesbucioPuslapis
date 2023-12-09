using System.ComponentModel.DataAnnotations;

namespace ViesbucioPuslapis.Models
{
    public class Room
    {
        [Key]
        public string kambario_numeris { get; set; }
        public int kambario_užimtumas { get; set; }
        public double nakties_kaina { get; set; }
        public int vietu_kiekis { get; set; }
        public int tipas { get; set; }
    }
}
