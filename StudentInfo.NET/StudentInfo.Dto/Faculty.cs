using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace StudentInfo.Dto
{
    public class Faculty
    {
        [Key]
        [Required]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Name { get; set; }

        public ICollection<Department> Departments { get; set; }
    }
}
