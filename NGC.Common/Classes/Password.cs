using System;
using System.Collections.Generic;
using System.Text;

namespace NGC.Common.Classes
{
    public class Password
    {
        public string Salt { get; set; }

        public string Hash { get; set; }
    }
}
