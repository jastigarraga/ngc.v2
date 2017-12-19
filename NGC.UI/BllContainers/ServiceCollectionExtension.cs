using Microsoft.Extensions.DependencyInjection;
namespace NGC.UI.BllContainers
{
    public static class ServiceCollectionExtension
    {
        public static void ConfigureBLLContainers(this IServiceCollection services)
        {
            services.AddTransient<IPhotoControllerBLL, PhotoControllerBLL>();
        }
    }
}
