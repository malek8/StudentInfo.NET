﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentInfo.Enums;

namespace StudentInfo.Faculties
{
    public class Program
    {
        [Key]
        [Required]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(125)]
        public string Name { get; set; }

        public ProgramLevel Level { get; set; }

        public DegreeType DegreeType { get; set; }

        public ProgramStudyType StudyType { get; set; }

        public Department Department { get; set; }
    }
}
