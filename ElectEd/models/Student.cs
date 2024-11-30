using System.ComponentModel.DataAnnotations;

public class Student
{
    public int Id { get; set; }

    [Required]
    [MaxLength(10)]
    public string StudentId { get; set; } // Unique Student ID

    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    [Required]
    [MaxLength(20)]
    public string Department { get; set; }

    public ICollection<VoteSlip> VoteSlips { get; set; } = new List<VoteSlip>();
}
