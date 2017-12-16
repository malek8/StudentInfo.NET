using StudentInfo.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentInfo.Students.Models;
using StudentInfo.Dto;
using StudentInfo.Helpers;

namespace StudentInfo.Students
{
    public class StudentPaymentService
    {
        private StudentInfoContext _db;

        public StudentPaymentService()
        {
            _db = new StudentInfoContext();
        }

        public bool MakePayment(MakePaymentModel model)
        {
            if (ValidateMakePayment(model))
            {
                var payment = _db.Payments.Find(model.PaymentId);
                if (payment != null)
                {
                    var transaction = new Transaction
                    {
                        Id = Guid.NewGuid(),
                        Amount = model.Amount,
                        CardNumber = model.CardNumber,
                        PaymentMethod = model.Method,
                        Date = DateTime.Now,
                        Payment = payment
                    };

                    _db.Transactions.Add(transaction);

                    try
                    {
                        _db.SaveChanges();
                        return true;
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            return false;
        }

        public bool InitTermPayment(Guid studentId)
        {
            var currentTerm = CourseHelper.CurrentTerm();

            if (!_db.Payments.Any(x => x.Term == currentTerm && x.Student.Id == studentId))
            {
                var student = _db.Students.Find(studentId);

                if (student != null)
                {
                    var charges = GetProgramCharges(student.Program.Id);

                    var payment = new Payment
                    {
                        Id = Guid.NewGuid(),
                        Student = student,
                        Term = currentTerm,
                        Items = charges,
                        Date = DateTime.Now
                    };

                    _db.Payments.Add(payment);

                    try
                    {
                        _db.SaveChanges();
                        return true;
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            else
            {
                return true;
            }

            return false;
        }

        public bool HasBalance(Guid studentId)
        {
            var student = _db.Students.Find(studentId);
            if (student != null)
            {
                var payments = _db.Payments.Where(x => x.Student.Id == student.Id).ToList();

                foreach(var p in payments)
                {
                    if (p.TotalToPay > p.TotalPaid)
                    {
                        return true;
                    }
                }

                return false;
            }

            return false;
        }

        public decimal GetBalance(Guid studentId)
        {
            var student = _db.Students.Find(studentId);
            var currentTerm = CourseHelper.CurrentTerm();
            if (student != null && student.Payments != null)
            {
                var currentPayment = student.Payments.FirstOrDefault(x => x.Term == currentTerm);
                var toPay = currentPayment.Items.Sum(x => x.Amount);

                if (currentPayment.Transactions == null)
                {
                    return toPay;
                }

                var paid = currentPayment.Transactions.Sum(x => x.Amount);
                return toPay - paid;
            }
            return 0;
        }

        public IList<Payment> GetPayments(Guid studentId)
        {
            var student = _db.Students.Find(studentId);
            if (student != null)
            {
                return student.Payments;
            }

            return new List<Payment>();
        }

        public Payment GetPayment(Guid id)
        {
            return _db.Payments.Find(id);
        }

        private IList<PaymentItem> GetProgramCharges(Guid programId)
        {
            var charges = new List<PaymentItem>();
            var program = _db.Programs.Find(programId);
            decimal termCharges = 0;

            if (program != null)
            {
                termCharges = program.TermCost;
            }

            charges.Add(new PaymentItem
            {
                Id = Guid.NewGuid(),
                Title = "Compulsory Fees",
                Amount = 582.92M
            });

            charges.Add(new PaymentItem
            {
                Id = Guid.NewGuid(),
                Title = "Tuition",
                Amount = termCharges
            });

            return charges;
        }

        private bool ValidateMakePayment(MakePaymentModel model)
        {
            if (model.Amount <= 0 ||
                string.IsNullOrEmpty(model.Method) ||
                string.IsNullOrEmpty(model.CardNumber) ||
                model.CardNumber.Length != 4 ||
                model.Method.Length < 3)
                return false;
            return true;
        }
    }
}
