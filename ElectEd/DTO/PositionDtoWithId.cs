using System.ComponentModel.DataAnnotations;
using Models;

namespace ElectEd.DTO
{
    public class PositionDtoWithId
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Title { get; set; }

        [Required]
        public int ElectionId { get; set; } // Foreign Key
        public Election Election { get; set; }
        [Required]
        public int MaxSelection { get; set; }
    }
}
