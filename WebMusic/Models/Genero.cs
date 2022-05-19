using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebMusic.Models
{
    public class Genero
    {
        public int Id { get; set; }

        [MaxLength(50, ErrorMessage="El campo {0} debe contener al menos un caracter")]
        [Required]
        public string des_genero { get; set; }

        public ICollection<Banda> Bandas { get; set; }
        [DisplayName("Cantidad de bandas")]
        public int BandasNumber => Bandas == null ? 0 : Bandas.Count;
    }
}
