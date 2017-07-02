using AutoMapper;
using NGC.Model;
using NGC.UI.Models;
using System.Linq;
namespace NGC.UI.Mapper
{
    public static class MapperFactory
    {
        private static IMapper instance;

        private static IMapperConfigurationExpression MapUser(this IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<User, UserModel>();
            cfg.CreateMap<UserModel, User>()
                .ForMember(u => u.Salt, opts => opts.Ignore());

            return cfg;
        }
        private static IMapperConfigurationExpression MapCustomer(this IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Customer, CustomerModel>();
            cfg.CreateMap<CustomerModel, Customer>()
                .ForMember(c => c.Template, opts => opts.Ignore());

            return cfg;
        }
        private static IMapperConfigurationExpression MapEmailTemplate(this IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<EmailTemplate, EmailTemplateModel>()
                .ForMember(u => u.Customers, opts => opts.MapFrom(u => u.Customers.Select(c => c.Id)));
            cfg.CreateMap<EmailTemplateModel, EmailTemplate>()
                .ForMember(u => u.Customers, opts => opts.Ignore());
            return cfg;
        }
        public static IMapper GetMapper()
        {
            return instance ?? Create();
        }
        private static IMapper Create()
        {
            MapperConfiguration configuration = new MapperConfiguration(cfg =>
            {
                cfg.MapUser()
                    .MapCustomer()
                    .MapEmailTemplate();
            });
            configuration.AssertConfigurationIsValid();
            instance = configuration.CreateMapper();
            return instance;
        }
    }
}
