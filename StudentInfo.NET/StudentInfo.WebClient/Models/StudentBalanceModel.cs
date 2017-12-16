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

        [Display(Name = "Amount to pay CAD")]
        [DataType(DataType.Currency)]
        public decimal AmountToPay { get; set; }

        [Display(Name = "Name on Card")]
        public string NameOnCreditCard { get; set; }

        [Display(Name = "Provider")]
        public string CreditProvider { get; set; }

        [Display(Name = "Card Number")]
        public string CreditCardNumber { get; set; }

        [Display(Name = "Card CVV")]
        public string CreditCardCode { get; set; }

        [Display(Name = "Expiration Month")]
        public string ExpirationMonth { get; set; }

        [Display(Name = "Expiration Year")]
        public string ExpirationYear { get; set; }

        public bool HasBalance
        {
            get { return Balance > 0; }
        }

        public Guid PaymentId { get; set; }
    }
}