using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class Position
{
    public int Id { get; set; }

    [Required]
    [MaxLength(255)]
    public string Title { get; set; }

    [Required]
    public int ElectionId { get; set; } // Foreign Key

    [Required]
    public int MaxSelection { get; set; }

}
