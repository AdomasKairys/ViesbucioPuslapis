using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ViesbucioPuslapis.Models
{
    public class ParkingPlace
    {
        [Key]
        public string vietos_id { get; set; }
        public DateTime užimta_nuo { get; set; }
        public DateTime užimta_iki { get; set; }
        public bool vietos_užimtumas { get; set; }
        public int aukstas_id { get; set; }
    }
}
