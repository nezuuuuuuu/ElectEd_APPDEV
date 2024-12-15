﻿using System.ComponentModel.DataAnnotations;
using Models;

namespace ElectEd.DTO
{
    public class StudentDto
    {


        [Required]

        public string StudentId { get; set; } // Unique Student ID
        [Required]


        public string Name { get; set; }
        [Required]


        public string Department { get; set; }
    }
}
