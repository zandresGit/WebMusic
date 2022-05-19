using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebMusic.Models
{
    public class Album
    {
        public int Id { get; set; }

        [MaxLength(50, ErrorMessage = "El campo {0} debe contener al menos un caracter")]
        [Required]
        public string nombre { get; set; }
        [Required]
        public int anio { get; set; }

        public ICollection<Cancion> Cancions { get; set; }
        [DisplayName("Cantidad de Canciones")]
        public int CancionsNumber => Cancions == null ? 0 : Cancions.Count;
    }
}
