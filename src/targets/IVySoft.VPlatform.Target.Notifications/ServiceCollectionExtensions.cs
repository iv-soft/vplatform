using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace IVySoft.VPlatform.Target.Notifications
{
    public static class ServiceCollectionExtensions
    {
        public static void UseNotifications(this IServiceCollection services)
        {
            services.AddTransient<IEmailSender, MailMessageSender>();
        }

    }
}
