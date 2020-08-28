using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using UdlånsWeb.Models;

namespace UdlånsWeb.DataHandling
{
    public class MailSending
    {
        public static void Email(string MailContent, List<User> Recipients)
        {
            // rewrite to zbc mail instead
            // rewrite to handle multiple admins
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress("FromMailAddress");
                foreach (User user in Recipients)
                {
                    if (user.Admin == true)
                    {
                        message.Bcc.Add(user.Email);
                    }
                    else
                    {
                        message.To.Add(user.Email);
                    }
                }
                message.Subject = "Lån af host";
                message.IsBodyHtml = true; //to make message body as html  
                message.Body = MailContent;
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com"; //for gmail host  
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("FromMailAddress", "password"); //Credentials to login to email 
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
            }
            catch (Exception)
            {
            }
        }
    }
}
