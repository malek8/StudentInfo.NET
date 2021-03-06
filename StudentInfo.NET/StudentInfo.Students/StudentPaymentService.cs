﻿using StudentInfo.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentInfo.Students.Models;
using StudentInfo.Dto;
using StudentInfo.Helpers;
using StudentInfo.Enums;

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

        public bool InitTermPayment(Guid studentId, Term term, int year)
        {
            if (!_db.Payments.Any(x => x.Term == term && x.Date.Year == year && x.Student.Id == studentId))
            {
                var student = _db.Students.Find(studentId);

                if (student != null)
                {
                    var charges = GetProgramCharges(student.Program.Id);
                    var paymentDate = new DateTime(year, DateTime.Now.Month, DateTime.Now.Day);

                    var payment = new Payment
                    {
                        Id = Guid.NewGuid(),
                        Student = student,
                        Term = term,
                        Items = charges,
                        Date = paymentDate,
                        ModifiedDate = DateTime.Now,
                        DueDate = StudentHelper.DueDate(term, year)
                    };

                    _db.Payments.Add(payment);

                    try
                    {
                        _db.SaveChanges();
                        return true;
                    }
                    catch (Exception ex)
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

        public bool InitFirstPayment(Guid studentId)
        {
            var student = _db.Students.Find(studentId);
            
            return InitTermPayment(studentId, student.StartTerm, student.StartYear);
        }

        public bool HasBalance(Guid studentId)
        {
            var student = _db.Students.Find(studentId);
            if (student != null)
            {
                var currentDate = DateTime.Now;
                var payments = _db.Payments.Where(x => x.Student.Id == student.Id && x.DueDate <= currentDate).ToList();

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
            var payments = GetPayments(studentId);

            if (payments != null)
            {
                return payments.Sum(x => x.Balance);
            }
            return 0;
        }
        
        public IList<Payment> GetPayments(Guid studentId)
        {
            var student = _db.Students.Find(studentId);
            if (student != null)
            {
                if (student.Payments.Sum(x => x.Balance) > 0)
                {
                    if (StudentHelper.IsPaymentDue())
                    {
                        foreach(var p in student.Payments)
                        {
                            if (p.Balance > 0)
                            {
                                AddInterest(p);
                            }
                        }
                    }
                }
                return student.Payments.OrderBy(x => x.DueDate).ToList();
            }

            return new List<Payment>();
        }

        public Payment GetPayment(Guid id)
        {
            var payment = _db.Payments.Find(id);
            if (payment != null)
            {
                if (payment.Balance > 0)
                {
                    AddInterest(payment);
                }

                return payment;
            }
            return new Payment();
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
                Amount = 582.92M,
                Order = 2
            });

            charges.Add(new PaymentItem
            {
                Id = Guid.NewGuid(),
                Title = "Tuition",
                Amount = termCharges,
                Order = 1
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

        private void AddInterest(Payment payment)
        {
            var numOfDay = DateTime.Now.Subtract(payment.DueDate).Days;
            if (numOfDay > 0)
            {
                var interestPaymentItem = payment.Items.FirstOrDefault(x => x.Title.Contains("interest"));
                if (interestPaymentItem != null)
                {
                    if (interestPaymentItem.NumberOfDays.HasValue && 
                        interestPaymentItem.NumberOfDays.Value < numOfDay)
                    {
                        interestPaymentItem.NumberOfDays = numOfDay;
                        var newInerestAmount = (payment.Balance * 0.03M) * (numOfDay - interestPaymentItem.NumberOfDays);
                        interestPaymentItem.Amount += newInerestAmount.Value;
                        interestPaymentItem.Title = $" 3% interest charges for {numOfDay} days";
                    }
                }
                else
                {
                    interestPaymentItem = new PaymentItem
                    {
                        Id = Guid.NewGuid(),
                        Amount = (payment.Balance * 0.03M) * numOfDay,
                        Title = $" 3% interest charges for {numOfDay} days",
                        NumberOfDays = numOfDay,
                        Order = 50
                    };

                    payment.Items.Add(interestPaymentItem);
                }

                try
                {
                    payment.ModifiedDate = DateTime.Now;
                    _db.SaveChanges();
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
