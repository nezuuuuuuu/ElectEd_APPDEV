using System.ComponentModel.DataAnnotations;

namespace ElectEd.DTO
{
    public class PositionDto
    {
        [Required]

        public string Title { get; set; }
        [Required]

        public int ElectionId { get; set; } // Foreign Key
        [Required]


        public int MaxSelection { get; set; }
    }
}
