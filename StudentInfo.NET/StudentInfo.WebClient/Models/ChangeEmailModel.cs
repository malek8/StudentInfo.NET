using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StudentInfo.WebClient.Models
{
    public class ChangeEmailModel
    {
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}