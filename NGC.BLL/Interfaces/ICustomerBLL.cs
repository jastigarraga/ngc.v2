using NGC.Common.Classes;
using NGC.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace NGC.BLL.Interfaces
{
    public interface ICustomerBLL : IBaseBLL<Customer>
    {
        Customer GetById(int id);
        DataSourceResult<Customer> GetByFilters(Customer filter, int page, int pageSize);

        IEnumerable<Customer> GetByDate(DateTime dateTime);
        IEnumerable<Customer> GetBYDayOfYear(int dayOfYear);
    }
}
