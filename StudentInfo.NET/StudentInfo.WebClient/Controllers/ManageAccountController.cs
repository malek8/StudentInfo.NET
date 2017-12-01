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

namespace StudentInfo.WebClient.Controllers
{
    [RequireHttps]
    [Authorize]
    public class ManageAccountController : Controller
    {
        private ApplicationUserManager _userManager;
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

        // GET: ManageAccount
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
                        var model = new RegisterViewModel
                        {
                            Email = user.Email,
                            FirstName = user.FirstName,
                            MiddleName = user.MiddleName,
                            LastName = user.LastName
                        };

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
    }
}