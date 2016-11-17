using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using WhoLends.Data;
using WhoLends.ViewModels;

namespace WhoLends.Web.Helpers
{
    public static class Mailer
    {
        public static void SendRegistrationConfirmation(User userobj, string callbackUrl)
        {
            MailMessage mailMsg = new MailMessage();

            // Init SmtpClient
            SmtpClient smtpClient = new SmtpClient();
            NetworkCredential credentials = (NetworkCredential)smtpClient.Credentials;

            // To
            mailMsg.To.Add(new MailAddress(userobj.Email, userobj.UserName));

            // From
            mailMsg.From = new MailAddress(credentials.UserName, "WhoLends");
            
            mailMsg.Subject = "Confirm your account";
            string text = "Please confirm your account by clicking " + callbackUrl;
            string html = $"<p>Please confirm your account by clicking <a href=\"{callbackUrl}\">here</a></p>";
            mailMsg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(text, null, MediaTypeNames.Text.Plain));
            mailMsg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(html, null, MediaTypeNames.Text.Html));

            //send mail
            smtpClient.Send(mailMsg);
        }

        public static void SendPasswordReset(ApplicationUser userobj, string callbackUrl)
        {
            MailMessage mailMsg = new MailMessage();

            // Init SmtpClient
            SmtpClient smtpClient = new SmtpClient();
            NetworkCredential credentials = (NetworkCredential)smtpClient.Credentials;

            // To
            mailMsg.To.Add(new MailAddress(userobj.Email, userobj.UserName));

            // From
            mailMsg.From = new MailAddress(credentials.UserName, "WhoLends");


            mailMsg.Subject = "Reset Password";
            string text = "Please reset your password by clicking " + callbackUrl;
            string html = "<p>Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a></p>";
            mailMsg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(text, null, MediaTypeNames.Text.Plain));
            mailMsg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(html, null, MediaTypeNames.Text.Html));

            //send mail
            smtpClient.Send(mailMsg);
        }
    }
}