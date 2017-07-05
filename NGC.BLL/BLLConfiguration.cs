using Microsoft.Extensions.DependencyInjection;
using NGC.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace NGC.BLL
{
    public static class BLLConfiguration
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddTransient<ICustomerBLL, CustomerBLL>();
            services.AddTransient<IUserBLL, UserBLL>();
            services.AddTransient<IConfigurationBLL, ConfigurationBLL>();
            services.AddTransient<IEmailTemplateBLL, EmailTemplateBLL>();
            services.AddTransient<IMerakiTextImageBLL, MerakiTextImageBLL>();
        }
        public static IServiceCollection ConfigureBLL(this IServiceCollection services)
        {
            Configure(services);
            return services;
        }
    }
}
