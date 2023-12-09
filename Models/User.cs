using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ViesbucioPuslapis.Models
{
    public class User
    {
        [Key]

        [Required]
        public int id_Naudotojas { get; set; }

        [Required]
        public string naudotojo_vardas { get; set; }

        [Required]
        public string naudotojo_pavarde { get; set; }

        [Required]
        public string elektroninis_paštas { get; set; }

        [Required]
        public string slaptažodis { get; set; }

    }
}
