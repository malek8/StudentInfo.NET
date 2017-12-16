using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInfo.Dto
{
    public class Payment
    {
        public Guid Id { get; set; }
        public Enums.Term Term { get; set; }
        public DateTime Date { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        
        public virtual IList<PaymentItem> Items { get; set; }

        public decimal TotalToPay => Items.Sum(x => x.Amount);

        public decimal TotalPaid => Transactions.Sum(x => x.Amount);

        public decimal Balance => TotalToPay - TotalPaid;

        public virtual Student Student { get; set; }

        public virtual IList<Transaction> Transactions { get; set; }
    }

    public class PaymentItem
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public decimal Amount { get; set; }
        public int Order { get; set; }
        public virtual Payment Payment { get; set; }
    }
}
