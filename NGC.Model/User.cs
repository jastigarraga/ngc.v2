using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;

namespace NGC.Model
{
    public class User : Entity, IIdentity
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        public byte[] Salt { get; set; }

        public string AuthenticationType => "Simple";

        public bool IsAuthenticated =>!string.IsNullOrWhiteSpace(Login);

        public string Name => Login;
    }
}
