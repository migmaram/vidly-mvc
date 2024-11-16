using System.ComponentModel.DataAnnotations;

namespace Vidly.Models
{
    public class Genre
    {
        public byte Id { get; set; }
        [Required]
        [StringLength(100)]
        public required string Name { get; set; }
    }
}
