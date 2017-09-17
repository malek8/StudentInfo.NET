using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace StudentInfo.WebClient.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        public string Index()
        {
            var gridMessage = new SendGridMessage();
            gridMessage.SetFrom(new EmailAddress(Settings.ConfirmationEmailFrom, Settings.SystemName));
            gridMessage.AddTo("malekatwiz@gmail.com");
            gridMessage.Subject = "Confirmation Email";

            var callbackUrl = "https://localhost:44319/Account/ConfirmEmail?userId=c4139425-907e-4e5a-958e-d7ad5f359b42&code=OTSf%2Bz3bzkAK5P7zp4xXy2mbmpqlxkCbyhcwEWwADIFmU0EuA20QXlQHVfCMoKTieuBaKepS%2Facs6vosDh9CQ5JbUWGAEqIfhQOOiu3z96BH3WcGLYt40taBim92L0dW8k%2F8%2BzlAwz9ESjjQqX4IWP5UOpqGHkaKQxFttm5%2FcmIrbYB%2FWfKw5tL8Dm93uz3j";
            var messageBody = $@"Click Here: <a href={callbackUrl}>Confirm</a>";
            gridMessage.AddContent(MimeType.Html, messageBody);

            var gridClient = new SendGridClient(Settings.MailerKey);
            gridClient.SendEmailAsync(gridMessage);

            return "Dead end";
        }
    }
}