using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using StudentInfo.Enums;

namespace StudentInfo.Dto
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
        [Display(Name = "Credits")]
        public int NumberOfCredits { get; set; }

        [Display(Name = "Level")]
        public ProgramLevel Level { get; set; }

        public virtual Department Department { get; set; }
    }
}
