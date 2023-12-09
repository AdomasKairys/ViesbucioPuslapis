using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ViesbucioPuslapis.Models
{
    public class Client
    {
        [Key]
        public int id_Naudotojas { get; set; }
        public string kliento_telefono_numeris { get; set; }
        public DateOnly kliento_gimimo_data { get; set; }
        public string kliento_lytis { get; set; }

    }
}
