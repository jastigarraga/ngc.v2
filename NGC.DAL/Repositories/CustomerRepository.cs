using NGC.Model;
using System;
using System.Collections.Generic;
using System.Text;
using NGC.DAL.Base;

namespace NGC.DAL.Repositories
{
    public class CustomerRepository : Base.BaseRepository<Customer>
    {
        public CustomerRepository(MerakiContext context) : base(context)
        {
        }
    }
}
