using System.ComponentModel.DataAnnotations;

namespace ViesbucioPuslapis.Models
{
    public class RoomTypes
    {
        [Key]
        public int id_Kambario_tipas { get; set; }
        public string name { get; set; }

    }
}
