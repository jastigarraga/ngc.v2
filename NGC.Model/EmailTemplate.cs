using System;
using System.Collections.Generic;
using System.Text;

namespace NGC.Model
{
    public class EmailTemplate : Entity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Subject { get; set; }
        public string Template { get; set; }
    }
}
