using System.ComponentModel.DataAnnotations;
using Models;

namespace ElectEd.DTO
{
    public class ElectionDto
    {
        [Required]

        public string Title { get; set; }
        [Required]

        public string ImagePath { get; set; }
        [Required]

        public string Description { get; set; }
        [Required]

        public List<string> Departments { get; set; } = new List<string> { "ALL" };
        [Required]

        public DateTime OpenDate { get; set; } = DateTime.Now;
        [Required]

        public DateTime CloseDate { get; set; } = DateTime.Now;


    }
}
