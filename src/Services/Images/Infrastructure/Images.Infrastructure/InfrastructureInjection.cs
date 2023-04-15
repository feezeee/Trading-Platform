﻿using Images.Core.Contracts.Infrastructure;
using Images.Infrastructure.Configurations;
using Images.Infrastructure.ImagesUploader;
using Microsoft.Extensions.DependencyInjection;

namespace Images.Infrastructure
{
    public static class InfrastructureInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<IImageUploader, ImageUploader>();
            return services;
        }
    }
}
