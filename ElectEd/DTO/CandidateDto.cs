using System.ComponentModel.DataAnnotations;
using Models;

namespace ElectEd.DTO
{
    public class CandidateDto
    {
           [Required]

        public string Name { get; set; }
        [Required]

        public string Partylist { get; set; }
        [Required]

        public string Year { get; set; }
        [Required]

        public string Course { get; set; }
        [Required]

        public string ImagePath { get; set; }
        [Required]

        public int ElectionId { get; set; }

        [Required]

        public int PositionId { get; set; }
        [Required]

        public int VoteCount { get; set; } = 0;
        [Required]

        public string Platforms { get; set; }
        [Required]

        public bool IsWinner { get; set; } = false;
    }
}
