﻿using System.ComponentModel.DataAnnotations;

namespace Models
{
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

    }
}
