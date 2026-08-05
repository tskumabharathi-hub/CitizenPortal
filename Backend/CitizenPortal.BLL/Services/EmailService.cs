using CitizenPortal.BLL.Interfaces;
using System.Net;
using System.Net.Mail;

namespace CitizenPortal.BLL.Services
{
    public class EmailService : IEmailService
    {
        public async Task<bool> SendIncidentNotification(string complaintId, string otp, string email)
        {
            bool bRes = true;
            try
            {
                var message = new MailMessage();

                message.From = new MailAddress("tskumabharathi@gmail.com");
                message.To.Add(email);

                message.Subject = "Citizen Portal - Incident Notification";

                message.Body = $"Your Incident : {complaintId} is successfully Registered. Concerned Team will reach you shortly.";

                using var smtp = new SmtpClient("smtp.gmail.com", 587);

                smtp.EnableSsl = true;

                smtp.Credentials =
                    new NetworkCredential(
                        "tskumabharathi@gmail.com",
                        "skdgolocgmzpjkck");

                await smtp.SendMailAsync(message);
            }
            catch (Exception ex)
            {
                bRes = false;
            }

            return bRes;
        }

        public async Task<bool> SendOtpAsync(string email, string otp)
        {
            bool bRes = true;
            try
            {
                var message = new MailMessage();

                message.From = new MailAddress("tskumabharathi@gmail.com");
                message.To.Add(email);

                message.Subject = "Citizen Portal - Email Verification";

                message.Body = $@"Hello," + "\n" + $"Your OTP is: {otp}" + "\n" + "This OTP is valid for 10 minutes." + "\n" + "Thank you,"+"\n"+"Citizen Portal";

                using var smtp = new SmtpClient("smtp.gmail.com", 587);

                smtp.EnableSsl = true;

                smtp.Credentials =
                    new NetworkCredential(
                        "tskumabharathi@gmail.com",
                        "skdgolocgmzpjkck");

                await smtp.SendMailAsync(message);
            }
            catch(Exception ex)
            {
                bRes = false;
            }

            return bRes;
        }
    }
}
