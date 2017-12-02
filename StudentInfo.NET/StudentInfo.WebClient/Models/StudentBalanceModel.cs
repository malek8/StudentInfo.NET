using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StudentInfo.WebClient.Models
{
    public class StudentBalanceModel
    {
        [Display(Name = "Balance CAD")]
        [DataType(DataType.Currency)]
        public decimal Balance { get; set; }

        [Display(Name = "Amount to pay in CAD")]
        [DataType(DataType.Currency)]
        public decimal AmountToPay { get; set; }

        public bool HasBalance
        {
            get { return Balance > 0; }
        }
    }
}