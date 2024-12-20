﻿using System.ComponentModel.DataAnnotations;

using Models;

namespace ElectEd.DTO
{
    public class VoteSlipDto
    {

        [Required]

        public int StudentId { get; set; } // Foreign Key

        [Required]

        public int ElectionId { get; set; } // Foreign Key

        [Required]

        public string CandidateIds { get; set; } // Store as a comma-separated string
    }
}
