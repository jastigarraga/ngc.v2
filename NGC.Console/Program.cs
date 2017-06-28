using NGC.BLL;
using NGC.BLL.Interfaces;
using System;
using Microsoft.Extensions.DependencyInjection;
using NGC.DAL.Base;
using NGC.Model;
using System.Threading.Tasks;
using NGC.Common.Classes;

namespace NGC.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddDbContext<MerakiContext>()
                .AddEntityFrameworkMySql()
                .AddLogging()
                .ConfigureBLL()
                .BuildServiceProvider();

            ICustomerBLL customerBLL = serviceProvider.GetService<ICustomerBLL>();
            IConfigurationBLL configBLL = serviceProvider.GetService<IConfigurationBLL>();
            var emailConfig = configBLL.GetEmailConfiguration();
            var customers = customerBLL.GetByDate(DateTime.Now);
            foreach(var customer in customers)
            {

            }
        }
        private static async Task<bool> SendMailToCustomer(EmailConfiguration config,Customer customer, EmailTemplate template)
        {
            var body = template.Template.Replace("{0}", customer.Name).Replace("{1}", customer.Surname1)
                .Replace("{2}", customer.Surname2);
            return await Common.Helpers.EmailHelper.SendEmailAsync(config, customer.Email,customer.Name, template.Subject,body);
        }
    }
}