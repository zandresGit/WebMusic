using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace WebMusic.Models
{
    public class AlbumViewModel : Album
    {
        [Display(Name = "Image")]
        public IFormFile ImageFile { get; set; }
    }
}
