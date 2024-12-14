using System.ComponentModel.DataAnnotations;

namespace ElectEd.DTO
{
    public class ElectionDtoWithId
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Title { get; set; }

        public string ImagePath { get; set; } // Store path to the image

        public string Description { get; set; }

        [Required]
        public List<string> Departments { get; set; } = new List<string> { "ALL" };

        [Required]
        public DateTime OpenDate { get; set; } = DateTime.Now;

        [Required]
        public DateTime CloseDate { get; set; } = DateTime.Now;
    }
}
