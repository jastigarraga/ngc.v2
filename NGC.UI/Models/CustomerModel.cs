using System;
using static NGC.Model.Customer;

namespace NGC.UI.Models
{
    public class CustomerModel
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

    }
}
