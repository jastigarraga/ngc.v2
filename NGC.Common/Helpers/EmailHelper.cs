

using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using NGC.Common.Classes;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NGC.Common.Helpers
{
    public static class EmailHelper
    {

        public static async Task SendEmailAsync(EmailConfiguration config, string email,string name, string subject, string message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress(config.FromName, config.From));
            emailMessage.To.Add(new MailboxAddress(name, email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart("html") { Text = message };
            

            using (var client = new SmtpClient())
            {
                client.LocalDomain = "some.domain.com";
                await client.ConnectAsync(config.Host, int.Parse(config.Port.ToString()),(SecureSocketOptions)config.Socket).ConfigureAwait(false);
                await client.SendAsync(emailMessage).ConfigureAwait(false);
                await client.DisconnectAsync(true).ConfigureAwait(false);
            }
        }
        public static IEnumerable<object> GetSecureSocketOptions()
        {
            string[] names = new string[] { "Ninguno", "Auto", "Ssl", "Tls", "Tls (si disponible)" };
            short i = 0;
            foreach(string name in names)
            {
                yield return new { Value = i++, Text = name };
            }
        }
    }
}
