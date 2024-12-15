using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Models
{
    public class Position
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
