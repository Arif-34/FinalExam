using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Indingo.Models
{
    public class Post:BaseEntity
    {
        [Required]
        [StringLength(400)]
        [MaxLength(200)]
        [MinLength(2)]
        public string Tittle { get; set; }
        [Required]
        [StringLength(1000)]
        [MaxLength(1000)]
        [MinLength(2)]
        public string Description { get; set; }
        public string? Image { get; set; }
        [NotMapped]
        public IFormFile? Photo { get; set; }
    }
}
