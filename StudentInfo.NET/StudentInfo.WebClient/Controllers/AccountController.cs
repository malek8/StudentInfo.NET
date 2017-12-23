using StudentInfo.WebClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using StudentInfo.WebClient.App_Start;
using StudentInfo.Dto;
using StudentInfo.Enums;
using StudentInfo.Data;
using System.Security.Claims;
using StudentInfo.Students;

namespace StudentInfo.WebClient.Controllers
{
    [RequireHttps]
    [Authorize]
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private StudentService _studentService;
        private StudentPaymentService _studentPaymentService;

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

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

        public AccountController()
        {
            _studentService = new StudentService();
            _studentPaymentService = new StudentPaymentService();
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            _studentService = new StudentService();
            _studentPaymentService = new StudentPaymentService();
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: true);

            switch(result)
            {
                case SignInStatus.Success:
                    return await CheckAccount(model.Email, returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }

        [Authorize]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    MiddleName = model.MiddleName,
                    LastName = model.LastName
                };

                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    UserManager.AddToRole(user.Id, model.Role);
                    if (model.Role == SystemRoles.Student)
                    {
                        var studentId = _studentService.CreateStudent(user.Id, model.ProgramId, model.Term, model.Year);
                        _studentPaymentService.InitTermPayment(studentId);
                    }
                    else if (model.Role == SystemRoles.Instructor)
                    {
                        AddTeacher(Guid.Parse(user.Id));
                    }

                    FixAccounts();
                    await SendConfirmationEmail(user.Id);
                    
                    return RedirectToAction("Index", "Home");
                }
                AddErrors(result);
            }

            return View(model);
        }

        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }

            var result = await UserManager.ConfirmEmailAsync(userId, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        [AllowAnonymous]
        public async Task<ActionResult> RequestConfirmationEmail(string userId)
        {
            await SendConfirmationEmail(userId);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Login", "Account");
        }

        private async Task<ActionResult> CheckAccount(string email, string returnUrl)
        {
            var user = await UserManager.FindByNameAsync(email);

            if (user != null && user.EmailConfirmed)
            {
                var identity = await UserManager.ClaimsIdentityFactory.CreateAsync(UserManager, user, DefaultAuthenticationTypes.ApplicationCookie);

                var student = _studentService.FindByUserId(user.Id);
                if (student != null)
                {
                    HttpContext.Session.Add("studentId", student.Id);

                    if (_studentPaymentService.HasBalance(student.Id))
                    {
                        HttpContext.Session.Add("student", student);
                        HttpContext.Session.Add("hasBalance", true);
                        HttpContext.Session.Add("owingBalance", _studentPaymentService.GetBalance(student.Id));
                    }
                    else
                    {
                        HttpContext.Session.Add("hasBalance", false);
                    }

                }

                HttpContext.Session.Add("firstName", user.FirstName);
                HttpContext.Session.Add("emailAddress", user.Email);

                AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                AuthenticationManager.SignIn(identity);

                return RedirectToLocal(returnUrl);
            }
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("RequestConfirmationEmail", "Account", new { userId = user.Id});
        }

        private async Task SendConfirmationEmail(string userId)
        {
            var code = await UserManager.GenerateEmailConfirmationTokenAsync(userId);
            var callbackUrl = Url.Action("ConfirmEmail", "Account",
                new { userId = userId, code = code }, protocol: Request.Url.Scheme);

            var messageBody = $@"Click on the following link to confirm your email address: <a href={callbackUrl}>Click Here</a>";
            await UserManager.SendEmailAsync(userId, "Confirmation Email", messageBody);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        public ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        private void AddErrors(IdentityResult result)
        {
            foreach(var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private void FixAccounts()
        {
            var db = new StudentInfoContext();

            db.Database.ExecuteSqlCommand("UPDATE [dbo].[AspNetUsers] SET [Discriminator] = 'ApplicationUser'");
        }

        private void AddTeacher(Guid userId)
        {
            var db = new StudentInfoContext();

            if (!db.Teachers.Any(x => x.ApplicationUserId == userId))
            {
                db.Teachers.Add(new Teacher()
                {
                    Id = Guid.NewGuid(),
                    ApplicationUserId = userId
                });

                db.SaveChanges();
            }
        }
    }
}