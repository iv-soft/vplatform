using System;
using System.Threading;
using System.Threading.Tasks;

namespace IVySoft.VPlatform.Etl.Core
{
    public interface IEtlScheduler
    {
        void AddProcess<EtlTask>(TimeSpan period) where EtlTask : IETLProcess;

        Task StartAsync(CancellationToken cancellationToken);

        Task StopAsync(CancellationToken cancellationToken);
    }
}