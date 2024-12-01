using DataAcess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ExcellProcessor
{
    public static class Email
    {

        public static void SendEmail(Stream s, string emailAddress, string ccEmailAddress, string message, string body, string filename)
        {



            MailMessage msg = new MailMessage();
            var es = emailAddress.Split(";");
            foreach (var e in es)
            {
                msg.To.Add(new MailAddress(e));
            }
            if (ccEmailAddress != string.Empty)
            {
                msg.CC.Add(new MailAddress(ccEmailAddress));
            }
            var ms = message;
            msg.From = new MailAddress("EverGreenShuttersAdmin@sj-software-solutions.com", "ESO Admin - No Reply");
            msg.Subject = ms;
            msg.Body = body;
            msg.IsBodyHtml = true;
            msg.Priority = MailPriority.High;
            s.Position = 0;
            msg.Attachments.Add(new Attachment(s, filename));
            SendEmail(msg);

        }

        public static void SendEmail(Dictionary<string, MemoryStream> Attachements, string emailAddress, string ccEmailAddress, string message, string body)
        {



            MailMessage msg = new MailMessage();
            var es = emailAddress.Split(";");
            foreach (var e in es)
            {
                msg.To.Add(new MailAddress(e));
            }
            if (ccEmailAddress != string.Empty)
            {
                msg.CC.Add(new MailAddress(ccEmailAddress));
            }
            var ms = message;
            msg.From = new MailAddress("EverGreenShuttersAdmin@sj-software-solutions.com", "ESO Admin - No Reply");
            msg.Subject = ms;
            msg.Body = body;
            msg.IsBodyHtml = true;
            msg.Priority = MailPriority.High;
            foreach (var a in Attachements)
            {
                a.Value.Position = 0;
                msg.Attachments.Add(new Attachment(a.Value, a.Key));

            }
            SendEmail(msg);

        }



        public static void SendEmail(MailMessage msg)
        {
            SmtpClient client = new SmtpClient();
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential("EverGreenShuttersAdmin@sj-software-solutions.com", "A81A4D37-52AB-466B-97D1-C6CD0DF012BA");
            client.Port = 587;
            client.Host = "smtp.office365.com";
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.EnableSsl = true;
            try
            {
                client.Send(msg);

            }
            catch (Exception ex)
            {
                //dbHelpers.LogDBError(ex.Message);
            }
        }





    }

}
