using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInfo.Dto
{
    public class Transaction
    {
        public Guid Id { get; set; }
        public string PaymentMethod { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public decimal CardNumber { get; set; }
        public virtual Payment Payment { get; set; }
    }
}
