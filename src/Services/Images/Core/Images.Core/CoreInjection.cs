using Images.Core.Contracts;
using Images.Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Images.Core
{
    public static class CoreInjection
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            services.AddSingleton<IImageService, ImageService>();
            return services;
        }
    }
}
