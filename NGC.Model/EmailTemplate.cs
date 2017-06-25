using System;
using System.Collections.Generic;
using System.Text;

namespace NGC.Model
{
    public class EmailTemplate
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Subject { get; set; }
        public string Template { get; set; }
    }
}
