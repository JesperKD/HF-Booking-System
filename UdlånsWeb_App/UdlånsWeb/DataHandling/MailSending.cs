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
        public static void Email(string MailContent, UserViewModel Recipients)
        {
            // rewrite to zbc mail instead
            // rewrite to handle multiple admins
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress("NoReply@zbc.dk");
                foreach (User user in Recipients.Users)
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
                message.Subject = "Host Udlåns System";
                //message.IsBodyHtml = true; //to make message body as html  
                message.Body = MailContent;
                smtp.Port = 25;
                smtp.Host = "mail.efif.dk"; //for gmail host  
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
            }
            catch (Exception)
            {
            }
        }
    }
}
