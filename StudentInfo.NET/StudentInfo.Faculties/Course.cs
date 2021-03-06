﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using StudentInfo.Users.Dto;
using StudentInfo.Enums;

namespace StudentInfo.Faculties
{
    public class Course
    {
        [Key]
        [Required]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Name { get; set; }

        [Required]
        [MaxLength(12)]
        public string Code { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        [Required]
        public int NumberOfCredits { get; set; }

        public ProgramLevel Level { get; set; }

        public Department Department { get; set; }

    }
}
