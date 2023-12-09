using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ViesbucioPuslapis.Models
{
    public class User
    {
        [Key]
        public int id_Naudotojas { get; set; }
        public string naudotojo_vardas { get; set; }
        public string naudotojo_pavarde { get; set; }
        public string elektroninis_paštas { get; set; }

        public string slaptažodis { get; set; }

    }
}
