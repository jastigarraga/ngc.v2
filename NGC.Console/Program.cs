using NGC.BLL;
using NGC.BLL.Interfaces;
using System;
using Microsoft.Extensions.DependencyInjection;
using NGC.DAL.Base;
using NGC.Model;
using System.Threading.Tasks;
using NGC.Common.Classes;
using static System.Console;
using static NGC.Common.Helpers.EmailHelper;
using static NGC.Common.Helpers.SecurityHelper;

namespace NGC.Console
{
    class Program
    {
        private static IServiceProvider serviceProvider;
        static void Main(string[] args)
        {
            serviceProvider = new ServiceCollection()
                .AddDbContext<MerakiContext>()
                .AddEntityFrameworkMySql()
                .AddLogging()
                .ConfigureBLL()
                .BuildServiceProvider();

            if (string.IsNullOrWhiteSpace(args[0]))
            {
                PrintInstructions();
                return;
            }
            switch (args[0])
            {
                case "useradd":
                    if(args.Length > 2 && string.IsNullOrWhiteSpace(args[1]) && string.IsNullOrWhiteSpace(args[2]))
                    {
                        CreateUser(args[1], args[2]);
                    }
                    else
                    {
                        Write("syntax");//TODO: Add syntax
                    }
                    return;
            }
            Write("No se reconoce el comando");
            ICustomerBLL customerBLL = serviceProvider.GetService<ICustomerBLL>();
            IConfigurationBLL configBLL = serviceProvider.GetService<IConfigurationBLL>();
            var emailConfig = configBLL.GetEmailConfiguration();
            var customers = customerBLL.GetByDate(DateTime.Now);
            foreach(var customer in customers)
            {
            }
        }
        private static void PrintInstructions()
        {

        }
        private static void CreateUser (string login, string password)
        {
            System.Console.WriteLine("Creando usuario");
            IUserBLL _userBLL = serviceProvider.GetService<IUserBLL>();
            User user = new User()
            {
                Login = login
            };
            user.UsetRawPassword(password);
            _userBLL.Insert(user);
            _userBLL.Save();
            WriteLine("Hecho");
        }
        private static void ModUser(string login, string password)
        {
            System.Console.WriteLine("Modificando usuario");
            IUserBLL _userBLL = serviceProvider.GetService<IUserBLL>();
            User user = _userBLL.GetByLogin(login);
            if (user == null)
            {
                WriteLine("ERROR: No se encuentra el usuario");
                return;
            }
            user.UsetRawPassword(password);
            _userBLL.Save();
            WriteLine("Hecho");
        }
        private static async Task<bool> SendMailToCustomer(EmailConfiguration config,Customer customer, EmailTemplate template)
        {
            var body = template.Template.Replace("{0}", customer.Name).Replace("{1}", customer.Surname1)
                .Replace("{2}", customer.Surname2);
            return await Common.Helpers.EmailHelper.SendEmailAsync(config, customer.Email,customer.Name, template.Subject,body);
        }
    }
}