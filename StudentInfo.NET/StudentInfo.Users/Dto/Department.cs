using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace StudentInfo.Users.Dto
{
    public class Department
    {
        [Key]
        [Required]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Name { get; set; }

        public Faculty Faculty { get; set; }

        public ICollection<Program> Programs { get; set; }

        public ICollection<Course> Courses { get; set; }
    }
}
