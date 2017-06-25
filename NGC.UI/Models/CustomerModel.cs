using NGC.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NGC.UI.Models
{
    public class CustomerModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Surname1 { get; set; }

        public string Surname2 { get; set; }

        public string Email { get; set; }

        public DateTime? Date { get; set; }

        public DateTime? LastSent { get; set; }

        public CustomerModel()
        {

        }
        public CustomerModel(Customer customer)
        {
            this.Id = customer.Id;
            this.Name = customer.Name;
            this.Surname1 = customer.Surname1;
            this.Surname2 = customer.Surname2;
            this.LastSent = customer.LastSent;
            this.Date = customer.Date;
            this.Email = customer.Email;
        }
        public Customer ToCustomer(Customer customer = null)
        {
            customer = customer ?? new Customer();
            customer.Id = this.Id;
            customer.Name = this.Name;
            customer.Surname1 = this.Surname1;
            customer.Surname2 = this.Surname2;
            customer.LastSent = this.LastSent;
            customer.Date = this.Date;
            customer.Email = this.Email;
            return customer;
        }
    }
}
