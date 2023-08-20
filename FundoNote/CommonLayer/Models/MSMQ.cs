using Experimental.System.Messaging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace CommonLayer.Models
{
    public class MSMQ
    {
        MessageQueue fundoQ = new MessageQueue();
        public void sendData2Queue(string token)
        {
            fundoQ.Path = @".\private$\Token";
            if (!MessageQueue.Exists(fundoQ.Path))
            {
                MessageQueue.Create(fundoQ.Path);
            }
            fundoQ.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
            fundoQ.ReceiveCompleted += FundoQ_ReceiveCompleted; ;
            fundoQ.Send(token);
            fundoQ.BeginReceive();
            fundoQ.Close();
        }

        private void FundoQ_ReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            try
            {
                var msg = fundoQ.EndReceive(e.AsyncResult);
                string token = msg.Body.ToString();
                string body = $"<a style = \"color:#00802b; text-decoration: none; font-size:20px;\" href='https://localhost:44384/api/User/resetpassword/{token}'>Click me</a>";
                string subject = "Token For Reset Password";
                var smtp = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("amit40fakeemail@gmail.com", "dyezqtoboczpzppj"),
                    EnableSsl = true,
                };
                using (var message = new MailMessage("amit40fakeemail@gmail.com", "amit40fakeemail@gmail.com", subject, body))
                {
                    message.IsBodyHtml = true;
                    smtp.Send(message);
                }
                fundoQ.BeginReceive();
            }
            catch (MessageQueueException)
            {

                throw;
            }
        }
    }
}
