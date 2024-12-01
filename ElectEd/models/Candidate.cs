using System.ComponentModel.DataAnnotations;

public class Candidate
{
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    [MaxLength(100)]
    public string Partylist { get; set; }

    [Required]
    [MaxLength(20)]
    public string Year { get; set; }

    public string Course { get; set; }

    public string ImagePath { get; set; } // Store path to the image

    [Required]
    public int ElectionId { get; set; } // Foreign Key

    [Required]
    public int PositionId { get; set; } // Foreign Key

    public int VoteCount { get; set; } = 0;

    public string Platforms { get; set; }
    public bool IsWinner { get; set; } = false;
}
