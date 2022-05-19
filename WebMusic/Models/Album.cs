using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WebMusic.Models
{
    public class Album
    {
        public int Id { get; set; }

        [MaxLength(50, ErrorMessage = "El campo {0} debe contener al menos un caracter")]
        [Required]
        [DisplayName("Nombre")]
        public string nombre { get; set; }
        [Required]
        [DisplayName("Año Lanzamiento")]
        public int anio { get; set; }

        public ICollection<Cancion> Cancions { get; set; }
        [DisplayName("Cantidad de Canciones")]
        public int CancionsNumber => Cancions == null ? 0 : Cancions.Count;

        [JsonIgnore] //lo ignora en la respuesta json
        [NotMapped] //no se crea en la base de datos
        public int IdBanda { get; set; }
    }
}
