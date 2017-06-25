using System;
using System.Collections.Generic;
using System.Text;

namespace NGC.Model
{
    public class Customer : Entity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Surname1 { get; set; }

        public string Surname2 { get; set; }

        public string Email { get; set; }

        public DateTime? Date { get; set; }

        public DateTime? LastSent { get; set; }
    }
}
