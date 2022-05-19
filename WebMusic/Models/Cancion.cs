using System.ComponentModel.DataAnnotations;

namespace WebMusic.Models
{
    public class Cancion
    {
        public int Id { get; set; }

        [MaxLength(50, ErrorMessage = "El campo {0} debe contener al menos un caracter")]
        [Required]
        public string Nombre { get; set; }

        [Required]
        public string Link { get; set; }

        [Required]
        public int Duracion { get; set; }
    }
}
