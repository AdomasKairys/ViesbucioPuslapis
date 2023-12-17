using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ViesbucioPuslapis.Models
{
    public class Client
    {
        [Key]

        [Required]
        public int id_Naudotojas { get; set; }

        [Required]
        public string kliento_telefono_numeris { get; set; }

        [Required]
        public DateTime kliento_gimimo_data { get; set; }

        [Required]
        public string kliento_lytis { get; set; }

    }
}
