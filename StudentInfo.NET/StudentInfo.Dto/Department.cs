using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace StudentInfo.Dto
{
    public class Department
    {
        [Key]
        [Required]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Name { get; set; }

        public virtual Faculty Faculty { get; set; }

        public virtual ICollection<Program> Programs { get; set; }

        public virtual ICollection<Course> Courses { get; set; }
    }
}
