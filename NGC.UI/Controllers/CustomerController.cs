﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using NGC.DAL.Base;
using NGC.BLL.Interfaces;
using NGC.Model;
using Microsoft.AspNetCore.Mvc;
using NGC.Common.Classes;
using NGC.UI.Models;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;

namespace NGC.UI.Controllers
{
    [Authorize]
    public class CustomerController : Base.MerakiController
    {
        public ICustomerBLL _customerBLL;
        public CustomerController(IUserBLL userBLL, ICustomerBLL customerBLL) : base(userBLL)
        {
            _customerBLL = customerBLL;
        }
        [HttpGet("api/customers")]
        public DataSourceResult<CustomerModel> GetAll(string filter, int page, int pageSize)
        {
            var model = Newtonsoft.Json.JsonConvert.DeserializeObject<CustomerModel>(filter ?? "{}");
            var customers = _customerBLL.GetByFilters(Mapper.Map<Customer>(model),page,pageSize);
            return new DataSourceResult<CustomerModel>(){
                Data = customers.Data.Select(c=>Mapper.Map<CustomerModel>(c)),
                Total = customers.Total
            };
        }
        [HttpPut("api/customers")]
        public CustomerModel CustomersPut([FromBody]CustomerModel model)
        {
            Customer customer = Mapper.Map<Customer>(model);
            _customerBLL.Insert(customer);
            _customerBLL.Save();
            return Mapper.Map<CustomerModel>(customer);
        }
        [HttpPost("api/customers")]
        public CustomerModel CustomersPost([FromBody]CustomerModel model)
        {
            Customer customer = _customerBLL.GetById(model.Id);
            customer = Mapper.Map<CustomerModel,Customer>(model,customer);
            _customerBLL.Update(customer);
            _customerBLL.Save();
            return Mapper.Map<CustomerModel>(customer);
        }
        [HttpDelete("api/customers")]
        public bool CustomerDelete([FromBody]CustomerModel model)
        {
            Customer customer = _customerBLL.GetById(model.Id);
            _customerBLL.Delete(customer);
            _customerBLL.Save();
            return true;

        }
        public ActionResult Index()
        {
            return View("Customers");
        }
    }
}
