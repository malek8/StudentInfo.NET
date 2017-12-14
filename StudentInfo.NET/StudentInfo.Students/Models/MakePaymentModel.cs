using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInfo.Students.Models
{
    public class MakePaymentModel
    {
        public decimal Amount { get; set; }
        public string Method { get; set; }
        public string CardNumber { get; set; }
        public Guid PaymentId { get; set; }
    }
}
