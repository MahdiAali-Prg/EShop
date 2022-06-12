using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace EShop.Common.Services.EmailTools
{
    public class EmailSender
    {
        public static async Task<bool> SendAsync(string to, string content, string subject)
        {
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress("mahdi1383harold@outlook.com");
                message.To.Add(new MailAddress(to));
                message.Subject = subject;
                message.IsBodyHtml = true; //to make message body as html  
                message.Body = content;
                smtp.Port = 587;
                smtp.Host = "smtp.office365.com"; //for gmail host  
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = true;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Credentials = new NetworkCredential("mahdi1383harold@outlook.com", "**********");
                await smtp.SendMailAsync(message);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
