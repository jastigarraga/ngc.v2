using AutoMapper;
using NGC.Model;
using NGC.UI.Models;

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
        public static IMapper GetMapper()
        {
            return instance ?? Create();
        }
        private static IMapper Create()
        {
            MapperConfiguration configuration = new MapperConfiguration(cfg =>
            {
                cfg.MapUser()
                    .MapCustomer();
            });
            configuration.AssertConfigurationIsValid();
            instance = configuration.CreateMapper();
            return instance;
        }
    }
}
