using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace StudentInfo.Users.Dto
{
    public class Address
    {
        [Key]
        [Required]
        public Guid Id { get; set; }

        public string AddressLine1 { get; set; }

        public string AddressLine2 { get; set; }

        [MaxLength(50)]
        public string City { get; set; }

        [MaxLength(30)]
        public string Province { get; set; }

        [MaxLength(6)]
        public string PostalCode { get; set; }

        [MaxLength(50)]
        public string Country { get; set; }
    }
}
