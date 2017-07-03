using System;
using System.Collections.Generic;
using System.Linq;
using NGC.DAL.Base;
using NGC.Model;
using NGC.Common.Classes;
using Microsoft.EntityFrameworkCore;

namespace NGC.BLL
{
    public class CustomerBLL :Base.BaseBLL<Customer>, Interfaces.ICustomerBLL
    {
        public CustomerBLL(MerakiContext context) : base(context)
        {
        }

        public DataSourceResult<Customer> GetByFilters(Customer filter, int page, int pageSize)
        {
            bool isEmailEmpty = string.IsNullOrWhiteSpace(filter.Email);
            bool isNameEmpty = string.IsNullOrWhiteSpace(filter.Name);
            bool isS1Empty = string.IsNullOrWhiteSpace(filter.Surname1);
            bool isS2Empty = string.IsNullOrWhiteSpace(filter.Surname2);
            var customers = repository.QueryAll.Where(c =>
                (filter.Id == 0 || filter.Id == c.Id) &&
                (isEmailEmpty || c.Email.Contains(filter.Email)) &&
                (isNameEmpty || c.Name.Contains(filter.Name)) &&
                (isS1Empty || c.Surname1.ToLower().Contains(filter.Surname1.ToLower())) &&
                (isS2Empty || c.Surname2.ToLower().Contains(filter.Surname2.ToLower())) &&
                (filter.Date == null || c.Date.HasValue && filter.Date.Value.Date == c.Date.Value.Date)
            );
            return new DataSourceResult<Customer>()
            {
                Total = customers.Count(),
                Data = page != 0 && pageSize != 0?customers.Skip((page - 1) * pageSize).Take(pageSize):customers
            };
        }
        public IEnumerable<Customer> GetByDate(DateTime dateTime)
        {
            dateTime = dateTime.Date;
            return repository.QueryAll.Where(c => c.Date != null && c.Date.Value.DayOfYear == dateTime.DayOfYear).Include(u=>u.Template);
        }
        public IEnumerable<Customer> GetBYDayOfYear(int dayOfYear)
        {
            return repository.QueryAll.Where(c => (c.Date != null && c.Date.Value.DayOfYear == dayOfYear) && (c.LastSent == null || c.LastSent < DateTime.Now.Date))
                .Include(u => u.Template);
        }
        public Customer GetById(int id)
        {
            return repository.QueryAll.Where(u => u.Id == id).FirstOrDefault();
        }
    }
}
