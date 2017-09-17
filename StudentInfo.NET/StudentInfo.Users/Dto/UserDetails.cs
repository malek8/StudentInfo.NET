using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentInfo.Users.Dto
{
    public class UserDetails
    {
        [Key]
        [Required]
        public Guid Id { get; set; }

        public string HomePhone { get; set; }
        public string WorkPhone { get; set; }
        public string CellPhone { get; set; }

        public Address HomeAddress { get; set; }
        public Address MailAddress { get; set; }
    }
}
