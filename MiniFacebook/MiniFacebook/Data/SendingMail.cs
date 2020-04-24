using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;

namespace MiniFacebook.Data
{
    public static class SendingMail
    {
        public static void Send(string confirmationLink,string userEmail)
        {
            //instantiate mimemessage
            var message = new MimeMessage();

            //from address
            message.From.Add(new MailboxAddress("MiniFacebook", "facebookcontactapp@gmail.com"));
            //to address
            message.To.Add(new MailboxAddress("mostafa", userEmail));
            //subject
            message.Subject = "Mini facebook Confirmation Mail";
            //Body
            message.Body = new TextPart("plain")
            {
                Text = confirmationLink
            };

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, false);
                client.Authenticate("facebookcontactapp@gmail.com", "Macm012345");
                client.Send(message);
                client.Disconnect(true);
            }
        }

    }
}
