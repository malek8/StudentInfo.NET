using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StudentInfo.WebClient.Models
{
    public class AccountDetailsModel
    {
        public string Email { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Balance/Credit")]
        public decimal Balance { get; set; }
    }
}