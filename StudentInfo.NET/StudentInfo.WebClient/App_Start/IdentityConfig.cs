using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using StudentInfo.WebClient.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;
using StudentInfo.Dto;
using StudentInfo.Data.UserDbContext;

namespace StudentInfo.WebClient.App_Start
{
    public class ApplicationSignInManager : SignInManager<ApplicationUser, string>
    {
        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
            :base(userManager, authenticationManager)
        {

        }

        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
        {
            return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
        }
    }

    public class ApplicationUserManager: UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
            :base(store)
        {

        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context.Get<UserDbContext>()));

            manager.UserValidator = new UserValidator<ApplicationUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 8,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true
            };

            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 3;

            //manager.RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<ApplicationUser>
            //{
            //    MessageFormat = "Your security code is {0}"
            //});

            manager.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<ApplicationUser>
            {
                Subject = "Security Code",
                BodyFormat = "Your security code is {0}"
            });

            manager.EmailService = new EmailService();
            //manager.SmsService = new SmsService();

            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("StudentInfo.NET ASP.NET Identity"));
            }

            return manager;
        }
    }

    public class EmailService: IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            var gridMessage = new SendGridMessage();
            gridMessage.SetFrom(new EmailAddress(Settings.ConfirmationEmailFrom, Settings.SystemName));
            gridMessage.AddTo(message.Destination);
            gridMessage.Subject = message.Subject;

            gridMessage.AddContent(MimeType.Html, message.Body);

            var gridClient = new SendGridClient(Settings.MailerKey);
            return gridClient.SendEmailAsync(gridMessage);
        }
    }
}