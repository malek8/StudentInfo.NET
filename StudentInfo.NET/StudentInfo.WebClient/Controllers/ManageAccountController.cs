using Microsoft.AspNet.Identity.Owin;
using StudentInfo.WebClient.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using StudentInfo.Enums;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using StudentInfo.WebClient.Models;
using Microsoft.Owin.Security;
using StudentInfo.Data;
using StudentInfo.WebClient.Helpers;
using StudentInfo.Students;
using StudentInfo.Dto;
using StudentInfo.Students.Models;

namespace StudentInfo.WebClient.Controllers
{
    [RequireHttps]
    [Authorize]
    public class ManageAccountController : Controller
    {
        private ApplicationUserManager _userManager;
        private StudentPaymentService _studentPaymentService;
        private StudentService _studentService;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        private IAuthenticationManager AuthenticationManager
        {
            get { return HttpContext.GetOwinContext().Authentication; }
        }

        public ManageAccountController()
        {
            _studentPaymentService = new StudentPaymentService();
            _studentService = new StudentService();
        }

        public async Task<ActionResult> Index()
        {
            ViewBag.Title = "Account Manager";
            if (User.Identity.IsAuthenticated)
            {
                var emailAddress = ((ClaimsIdentity)User.Identity).FindFirstValue(CustomClaims.EmailAddress);

                if (!string.IsNullOrEmpty(emailAddress))
                {
                    var user = await UserManager.FindByEmailAsync(emailAddress);

                    if (user != null)
                    {
                        var model = new AccountDetailsModel
                        {
                            Email = user.Email,
                            FirstName = user.FirstName,
                            MiddleName = user.MiddleName,
                            LastName = user.LastName
                        };

                        if (User.IsInRole(SystemRoles.Student))
                        {
                            var student = _studentService.FindByUserId(user.Id);

                            if (student != null)
                            {
                                model.Balance = _studentPaymentService.GetBalance(student.Id);
                            }
                        }

                        return View(model);
                    }
                }
            }

            return HttpNotFound();
        }

        [ValidateAntiForgeryToken]
        public async Task<JsonResult> ChangeEmail(ChangeEmailModel model)
        {
            if (ValidateEmail(model.Email))
            {
                var oldEmailAddress = ((ClaimsIdentity)User.Identity).FindFirstValue(CustomClaims.EmailAddress);
                if (!string.IsNullOrEmpty(oldEmailAddress))
                {
                    var user = await UserManager.FindByEmailAsync(oldEmailAddress);

                    if (user != null)
                    {
                        user.Email = model.Email;
                        user.EmailConfirmed = false;
                        var result = UserManager.Update(user);

                        if (result.Succeeded)
                        {
                            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                            return Json(new { success = true });
                        }

                        var i = 0;
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(i.ToString(), error);
                            i++;
                        }
                    }
                }
            }

            ModelState.AddModelError("invalidInput", "Invalid email address");

            var errors = new List<string>();
            foreach (var e in ModelState.Values)
            {
                errors.AddRange(e.Errors.Select(x => x.ErrorMessage));
            }

            ModelState.Clear();
            return Json(errors);
        }

        [ValidateAntiForgeryToken]
        public async Task<JsonResult> ChangePassword(ChangePasswordModel model)
        {
            var errors = ValidatePassword(model.Password, model.ConfirmPassword);

            if (errors.Count == 0)
            {
                var emailAddress = ((ClaimsIdentity)User.Identity).FindFirstValue(CustomClaims.EmailAddress);
                if (!string.IsNullOrEmpty(emailAddress))
                {
                    var user = await UserManager.FindByEmailAsync(emailAddress);

                    if (user != null)
                    {
                        if (UserManager.CheckPassword(user, model.CurrentPassword))
                        {
                            var newHashedPassword = new PasswordHasher().HashPassword(model.Password);
                            user.PasswordHash = newHashedPassword;

                            var result = UserManager.Update(user);

                            if (result.Succeeded)
                            {
                                AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                                return Json(new { success = true });
                            }
                        }
                        else
                        {
                            errors.Add("Current password is not correct");
                        }
                    }
                }
            }

            return Json(errors);
        }

        [HttpGet]
        [AuthorizeRoles(SystemRoles.Student)]
        public ActionResult GetStudentBalance()
        {
            var model = new StudentBalanceModel();
            if (User.Identity.IsAuthenticated && User.IsInRole(SystemRoles.Student))
            {
                

                var db = new StudentInfoContext();
                var userId = Guid.Parse(User.Identity.GetUserId());

                var student = db.Students.FirstOrDefault(x => x.ApplicationUserId == userId);

                if (student != null && student.Balance > 0)
                {
                    model.Balance = student.Balance;
                }
            }
            return View("_PayBalance", model);
        }

        [HttpGet]
        [AuthorizeRoles(SystemRoles.Student)]
        public ActionResult BalanceToPay(Guid paymentId)
        {
            if (User.Identity.IsAuthenticated && User.IsInRole(SystemRoles.Student))
            {
                var payment = _studentPaymentService.GetPayment(paymentId);
                if (payment != null)
                {
                    var model = new StudentBalanceModel
                    {
                        Balance = payment.Balance,
                        PaymentId = paymentId
                    };

                    return View("_PayBalance", model);
                }
            }

            return HttpNotFound();
        }

        public JsonResult PayBalance(StudentBalanceModel model)
        {
            var messages = new List<string>();

            if (!ValidateCreditCardInformation(model))
            {
                messages.Add("Invalid card information");
            }
            else if (model.AmountToPay <= 0)
            {
                messages.Add("Invalid amount");
            }
            else
            {
                var payModel = new MakePaymentModel
                {
                    Amount = model.AmountToPay,
                    Method = model.CreditProvider,
                    CardNumber = model.CreditCardNumber.Substring(11, 4),
                    PaymentId = model.PaymentId
                };

                var result = _studentPaymentService.MakePayment(payModel);

                if (result)
                {
                    if (model.AmountToPay > model.Balance)
                    {
                        messages.Add($"You have been credited ${model.AmountToPay - model.Balance}");
                    }
                    else
                    {
                        messages.Add("Payment has been deposited successfully");
                    }

                    return Json(new { success = true, messages = messages });
                }
            }

            return Json(new { success = false, messages = messages });
        }

        [HttpGet]
        [AuthorizeRoles(SystemRoles.Student)]
        public ActionResult PaymentInquiry()
        {
            if (User.Identity.IsAuthenticated && User.IsInRole(SystemRoles.Student))
            {
                var studentIdAsStr = Helper.GetClaimValue(User.Identity, CustomClaims.StudentId);
                if (!string.IsNullOrEmpty(studentIdAsStr))
                {
                    var studentId = Guid.Parse(studentIdAsStr);
                    var payments = _studentPaymentService.GetPayments(studentId);

                    return View(payments);
                }
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [AuthorizeRoles(SystemRoles.Student)]
        public ActionResult PaymentItemDetails(Guid paymentId)
        {
            if (User.Identity.IsAuthenticated && User.IsInRole(SystemRoles.Student))
            {
                var payment = _studentPaymentService.GetPayment(paymentId);
                if (payment != null)
                {
                    return View(payment);
                }
                else
                {
                    return View(new Payment());
                }
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [AuthorizeRoles(SystemRoles.Student)]
        public ActionResult GetTransactions(Guid paymentId)
        {
            if (User.Identity.IsAuthenticated && User.IsInRole(SystemRoles.Student))
            {
                var payment = _studentPaymentService.GetPayment(paymentId);
                if (payment != null && payment.Transactions != null)
                {
                    return View("_Transactions", payment);
                }
            }

            return HttpNotFound();
        }

        private bool ValidateEmail(string email)
        {
            if (string.IsNullOrEmpty(email)) return false;
            if (email.Length > 256) return false;

            try
            {
                var address = new System.Net.Mail.MailAddress(email);
                return address.Address == email;
            }
            catch(Exception)
            {
                return false;
            }
        }

        private List<string> ValidatePassword(string password, string confirmPassword)
        {
            var errors = new List<string>();

            if (string.IsNullOrEmpty(password)) errors.Add("Password is required");

            if (!string.IsNullOrEmpty(password) && !string.IsNullOrEmpty(confirmPassword))
            {
                if (password.Length < 6) errors.Add("Password is too short");
                if (password.Length > 100) errors.Add("Password is too long");
                if (!password.Equals(confirmPassword, StringComparison.CurrentCulture))
                    errors.Add("Confirmation does not match password");
                if (password.Count(x => char.IsUpper(x)) < 2)
                    errors.Add("Password must contain at least 2 capital letters");
                if (password.Count(x => char.IsDigit(x)) == 0)
                    errors.Add("Password must contain at least 1 number");
                if (password.Count(x => !char.IsLetterOrDigit(x)) == 0)
                    errors.Add("Password must contain at least 1 special character");
            }

            return errors;
        }

        private bool ValidateCreditCardInformation(StudentBalanceModel model)
        {
            if (string.IsNullOrEmpty(model.NameOnCreditCard) ||
                string.IsNullOrEmpty(model.CreditProvider) ||
                string.IsNullOrEmpty(model.CreditCardNumber) ||
                string.IsNullOrEmpty(model.CreditCardCode) ||
                string.IsNullOrEmpty(model.ExpirationMonth) ||
                string.IsNullOrEmpty(model.ExpirationYear) ||
                model.NameOnCreditCard.Length <= 3 ||
                model.CreditCardCode.Length != 3 ||
                model.CreditCardNumber.Length != 16 ||
                !model.NameOnCreditCard.All(x => char.IsLetter(x)) ||
                !model.CreditCardNumber.All(x => char.IsNumber(x)) ||
                !model.CreditCardCode.All(x => char.IsNumber(x))
                ) return false;
            return true;
        }
    }
}