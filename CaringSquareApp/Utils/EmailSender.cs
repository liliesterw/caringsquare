using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.IO;

namespace CaringSquareApp.Utils
{
    public class EmailSender
    {

       
        private const String API_KEY = "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";

        public void Send(String toEmail, String subject, String contents)
        {
            var client = new SendGridClient(API_KEY);
            var from = new EmailAddress("admin@caringsquare.com", "Caring Square Application");
            var to = new EmailAddress(toEmail, "");
            var plainTextContent = contents;
            var htmlContent = "<p>" + contents + "</p>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

            string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            
            var response = client.SendEmailAsync(msg);
        }

        public void SendBulkEmail(List<String> toEmail, String subject, String contents)
        {
            var client = new SendGridClient(API_KEY);
            var from = new EmailAddress("admin@caringsquare.com", "Caring Square Application");
            //recipients.Select(x => MailHelper.StringToEmailAddress(x)).ToList();
            foreach (String tempEmail in toEmail)
            {

                var to = new EmailAddress(tempEmail, "");
                var plainTextContent = contents;
                var htmlContent = "<p>" + contents + "</p>";
                var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

                //var bytes = File.ReadAllBytes("C:\\Users\\LEGION\\Downloads\\event_report.pdf");
                //var file = Convert.ToBase64String(bytes);
                //msg.AddAttachment("event_report.pdf", file);
                var response = client.SendEmailAsync(msg);
            }

           
        }

    }
}