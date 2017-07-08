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
using System.IO;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Reflection;
using NGC.Common.Extensions;

namespace NGC.Console
{
    class Program
    {
        private static IServiceProvider serviceProvider;
        static void Main(string[] args)
        {
            foreach(string arg in args)
            {
                WriteLine(arg);
            }
            string d = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            d = Directory.GetParent(d).Parent.Parent.FullName;

            string settingsPath = Path.Combine(d, "appsettings.txt");
            if (!File.Exists(settingsPath))
            {
                Write($"No se encuentra {settingsPath}");
                return;
            }
            serviceProvider = new ServiceCollection()
                .AddDbContext<MerakiContext>()
                .AddEntityFrameworkMySql()
                .AddLogging()
                .AddSingleton<MerakiContext>(c => {
                    return new MerakiContext(File.ReadAllText(settingsPath));
                })
                .ConfigureBLL()
                .BuildServiceProvider();

            if (args.Length == 0 || string.IsNullOrWhiteSpace(args[0]))
            {
                PrintInstructions();
                return;
            }
            switch (args[0])
            {
                case "useradd":
                    if(args.Length > 2 && !string.IsNullOrWhiteSpace(args[1]) && !string.IsNullOrWhiteSpace(args[2]))
                    {
                        CreateUser(args[1], args[2]);
                    }
                    else
                    {
                        Write("Error de sintaxis");
                        WriteLine("     dotnet run useradd [login] [password]");
                    }
                    return;
                case "usermod":
                    if (args.Length > 2 && !string.IsNullOrWhiteSpace(args[1]) && !string.IsNullOrWhiteSpace(args[2]))
                    {
                        ModUser(args[1], args[2]);
                    }
                    else
                    {
                        Write("Error de sintaxis");
                        WriteLine("     dotnet run usermod [login] [password]");
                    }
                    return;
                case "birthdays":
                    SendMails();
                    return;
            }
            Write("No se reconoce el comando");
            
        }
        private static void SendMails()
        {
            ICustomerBLL customerBLL = serviceProvider.GetService<ICustomerBLL>();
            IConfigurationBLL configBLL = serviceProvider.GetService<IConfigurationBLL>();
            var emailConfig = configBLL.GetEmailConfiguration();
            var customers = customerBLL.GetBYDayOfYear(DateTime.Now.DayOfYear);
            WriteLine($"Hoy cumplen {customers.Count()} clientes.");
            foreach (var customer in customers)
            {
                SendMailToCustomer(emailConfig, customer).Wait();
            }
        }
        private static void PrintInstructions()
        {
            WriteLine(@"-Para crear un usuario
    dotnet run useradd [login] [password]
-Para modificar la contraseña de un usuario
    dotnet run usermod [login] [password]
-Para enviar felicitacione
    dotnet run birthdays");
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
        private static async Task<bool> SendMailToCustomer(EmailConfiguration config,Customer customer)
        {
            WriteLine($"Enviando a {customer.Email} (útlimo envío en {customer.LastSent?.ToString() ?? "[nunca]"})");
            var body = customer.Template.Template.Replace("{0}", customer.Name).Replace("{1}", customer.Surname1)
                .Replace("{2}", customer.Surname2);
            var imageBLL = serviceProvider.GetService<IMerakiTextImageBLL>();
            var images = imageBLL.GetAll();
            foreach (var image in images)
            {
                string placeholder = "{Image:" + image.Id + "}";
                if (body.Contains(placeholder))
                body.Replace(placeholder,$"<img src='data:image/png;base64,{image.Draw(customer)}");
            }
            var custBLL = serviceProvider.GetService<ICustomerBLL>();

            customer.LastSent = DateTime.Now;
            custBLL.Save();
            WriteLine("Enviado");
            return await Common.Helpers.EmailHelper.SendEmailAsync(config, customer.Email, customer.Name, customer.Template.Subject, body);
           
        }
    }
}