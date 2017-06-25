using NGC.Model;
using System.Linq;
using System.Collections.Generic;
using MailKit.Security;

namespace NGC.Common.Classes
{
    public class EmailConfiguration
    {
        public const string HOST = "EmailHost", PORT = "EmailPort",
            SSL = "EmailUseSsl", FROM = "EmailFrom", USER ="EmailUser",
            PASS="EmailPass" ,FROMNAME = "EmailFromName";
        public string Host { get; set; }
        public int Port { get; set; }
        public int Socket { get; set; }
        public string FromName { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string User { get; set; }

        public string Password { get; set; }

        public EmailConfiguration(IEnumerable<Configuration> config)
        {
            Host = config.Where(c => c.Key == HOST).FirstOrDefault()?.Value;
            int.TryParse(config.Where(c => c.Key == PORT).FirstOrDefault()?.Value, out int port);
            Port = port;
            int.TryParse(config.Where(c => c.Key == SSL).FirstOrDefault()?.Value, out int s);
            Socket = s;
            From = config.Where(c => c.Key == FROM).FirstOrDefault()?.Value;
            User = config.Where(c => c.Key == USER).FirstOrDefault()?.Value;
            Password = config.Where(c => c.Key == PASS).FirstOrDefault()?.Value;
            FromName = config.Where(c => c.Key == FROMNAME).FirstOrDefault()?.Value;
        }
        public IEnumerable<Configuration> ToConfigCollection(IEnumerable<Configuration> config = null)
        {
            yield return new Configuration() { Key = HOST, Value = Host };
            yield return new Configuration() { Key = PORT, Value = Port.ToString() };
            yield return new Configuration() { Key = SSL, Value = Socket.ToString()  };
            yield return new Configuration() { Key = FROM, Value = From };
            yield return new Configuration() { Key = USER, Value = User };
            yield return new Configuration() { Key = FROMNAME, Value = FromName };
            yield return new Configuration() { Key = PASS, Value = Password };
        }
        public EmailConfiguration() { }

        public override string ToString()
        {
            return $"<p>Host:{Host}</p><p>Puerto:{Port}</p><p>Ssl:{Socket}</p><p>Correo:{From}</p><p>Usurario:{User}</p><p>";
        }
    }
}
