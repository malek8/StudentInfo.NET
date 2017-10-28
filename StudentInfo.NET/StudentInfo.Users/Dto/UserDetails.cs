using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInfo.Users.Dto
{
    public class UserDetails
    {
        [Key]
        [Required]
        public Guid Id { get; set; }

        [MaxLength(25)]
        public string WorkPhone { get; set; }

        [MaxLength(25)]
        public string HomePhone { get; set; }

        [MaxLength(25)]
        public string CellPhone { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        public Address MailAddress { get; set; }
        public Address HomeAddress { get; set; }
        public IEnumerable<StudentCourse> StudentCourse { get; set; }
    }
}
