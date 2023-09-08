using Experimental.System.Messaging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace CommonLayer.Models
{
    public class SendColabAlartToColabEmail
    {
        public void EmailService(string email)
        {
			try
			{
                string fromEmail = "amit40fakeemail@gmail.com";

                string body = $"{fromEmail} Add You as an Colabaretor with his Note";
                string subject = "Colabaretion Info";
                var smtp = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential(fromEmail, "dyezqtoboczpzppj"),
                    EnableSsl = true,
                };
                using (var message = new MailMessage(fromEmail, email, subject, body))
                {
                    message.IsBodyHtml = true;
                    smtp.Send(message);
                }
            }
			catch (Exception)
			{

				throw;
			}
        }
    }
}
