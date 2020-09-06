using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace IVySoft.VPlatform.Etl.Core
{
    public static class ServiceCollectionExtensions
    {
        public static void UseEtlCore(this IServiceCollection services)
        {
            services.AddTransient<IEtlScheduler, EtlScheduler>();
        }

    }
}
