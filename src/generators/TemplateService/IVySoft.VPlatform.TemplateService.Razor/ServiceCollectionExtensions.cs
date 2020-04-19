using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace IVySoft.VPlatform.TemplateService.Razor
{
    public static class ServiceCollectionExtensions
    {
        public static void UseTemplateServiceRazor(this IServiceCollection services)
        {
            services.AddTransient<IRazorManager, RazorManager>();
            services.AddSingleton<IRazorRegistrator, RazorRegistrator>();
        }
    }
}
