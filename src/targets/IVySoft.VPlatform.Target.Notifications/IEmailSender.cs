using System;
using System.Threading.Tasks;

namespace IVySoft.VPlatform.Target.Notifications
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
