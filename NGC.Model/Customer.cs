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

        public bool Gender { get; set; }

        public string Email { get; set; }

        public DateTime? Date { get; set; }

        public DateTime? LastSent { get; set; }

        public MaritalStates MaritalState { get; set; }

        public int IdTemplate { get; set; }

        public int ChildrenCount { get; set; }

        public virtual EmailTemplate Template {get;set;}
        public enum MaritalStates
        {
            Unknown = 0,
            Single = 1,
            WithPartner = 2,
            Married = 3
        }
    }
}
