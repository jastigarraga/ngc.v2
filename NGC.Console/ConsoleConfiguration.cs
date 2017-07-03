using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Primitives;
using Microsoft.Extensions.Options;
using NGC.DAL.Base;

namespace NGC.Console
{
    public class ConsoleConfiguration : IOptions<MerakiConfiguration>
    {
        private MerakiConfiguration config;

        public ConsoleConfiguration (MerakiConfiguration c){
            config = c;
        }
        public MerakiConfiguration Value =>config ;
    }
}
