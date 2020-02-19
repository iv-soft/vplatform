using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace IVySoft.VPlatform.TemplateService.Entity
{
    public static class ServiceCollectionExtensions
    {
        public static void UseTemplateServiceEntity(this IServiceCollection services)
        {
            services.AddTransient<IEntityManager, EntityManager>();
            services.AddSingleton<IEntityModelHolder, EntityModelHolder>();
        }
    }
}
