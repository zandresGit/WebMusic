
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WebMusic.Models
{
    public class Banda
    {
        public int Id { get; set; }

        [MaxLength(50, ErrorMessage = "El campo {0} debe contener al menos un caracter")]
        [Required]
        [DisplayName("Nombre")]
        public string nombre { get; set; }
        [MaxLength(50, ErrorMessage = "El campo {0} debe contener al menos un caracter")]
        [Required]
        [DisplayName("Origen")]
        public string origen { get; set; }

        public ICollection<Album> Albums { get; set; }
        [DisplayName("Cantidad de Albumes")]
        public int AlbumsNumber => Albums == null ? 0 : Albums.Count;

        [JsonIgnore] //lo ignora en la respuesta json
        [NotMapped] //no se crea en la base de datos
        public int IdGenero { get; set; }
    }
}
