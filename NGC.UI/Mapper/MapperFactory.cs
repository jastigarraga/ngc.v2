using AutoMapper;
using NGC.Common.Extensions;
using NGC.Model;
using NGC.UI.Models;
using System;
using System.IO;
using System.Linq;
namespace NGC.UI.Mapper
{
    public static class MapperFactory
    {
        private static IMapper instance;
        private static IMapperConfigurationExpression MapMerakiTextImage(this IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<MerakiTextImage, MerakiTextImageModel>()
                .ForMember(i=>i.Src,opts=>opts.MapFrom(entity=>$"data:image/png;base64,{Convert.ToBase64String(entity.Bytes)}"));
            cfg.CreateMap<MerakiTextImageModel, MerakiTextImage>()
                .ForMember(entity=>entity.Bytes,opts=>opts.MapFrom(model=>Convert.FromBase64String(model.Src.Replace("data:image/png;base64,",""))));
            cfg.CreateMap<MerakiTextImage, TextImageComboModel>();

            return cfg;
        }
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
                .ForMember(c => c.Template, opts => opts.Ignore())
                .ForMember(c => c.Photos, opts=>opts.Ignore());

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
                    .MapEmailTemplate()
                    .MapMerakiTextImage()
                    .MapPhoto();
            });
            configuration.AssertConfigurationIsValid();
            instance = configuration.CreateMapper();
            return instance;
        }
        public static IMapperConfigurationExpression MapPhoto(this IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Photo, PhotoModel>()
                    .ForMember(m => m.File, opts => opts.Ignore())
                    .ForMember(m => m.src, opts => opts.ResolveUsing(e=>e.bytes?.ToBase64()));
            cfg.CreateMap<PhotoModel, Photo>()
                .ForMember(e => e.bytes, opts => opts.ResolveUsing(m => m.src?.FromBase64ToByteArray()))
                .ForMember(e => e.Customer,opts => opts.Ignore());

            return cfg;
        }
    }
}
