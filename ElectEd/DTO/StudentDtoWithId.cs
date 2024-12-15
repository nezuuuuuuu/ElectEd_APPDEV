using System.ComponentModel.DataAnnotations;
using Models;

namespace ElectEd.DTO
{
    public class StudentDtoWithId
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(10)]
        public string StudentId { get; set; } 
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(20)]
        public string Department { get; set; }
    }
}
