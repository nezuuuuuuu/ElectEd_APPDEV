﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class VoteSlip
    {
        public int Id { get; set; }

        [Required]
        public int StudentId { get; set; } // Foreign Key

        [Required]
        public int ElectionId { get; set; } // Foreign Key
        public Election Election { get; set; }
        [Required]
        public string CandidateIds { get; set; } // Store as a comma-separated string
    }
}
