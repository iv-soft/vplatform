using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;

namespace IVySoft.VPlatform.Etl.Core
{
    internal class EtlScheduler : IEtlScheduler
    {
        private readonly IServiceProvider sp_;

        interface IEtlProcessConfig
        {
            bool IsRunned { get; }
            DateTime NextStart { get; }

            void Start(IServiceProvider sp);
            Task Stop();
        };

        private readonly List<IEtlProcessConfig> tasks_ = new List<IEtlProcessConfig>();

        class EtlProcessConfig<EtlTask> : IEtlProcessConfig
            where EtlTask : IETLProcess
        {
            private readonly TimeSpan period_;
            private DateTime next_start_;
            private Task task_;

            public EtlProcessConfig(TimeSpan period)
            {
                this.period_ = period;
                this.next_start_ = DateTime.Now;
            }

            public bool IsRunned => (this.task_ != null);
            public DateTime NextStart => this.next_start_;

            public void Start(IServiceProvider sp)
            {
                lock (this)
                {
                    this.next_start_ = DateTime.Now + this.period_;

                    this.task_ = ActivatorUtilities.CreateInstance<EtlTask>(sp).Process(sp);
                    this.task_.ContinueWith(t =>
                    {
                        lock (this)
                        {
                            this.task_ = null;
                        }
                    });
                }
            }

            public async Task Stop()
            {
                while(this.task_ != null)
                {
                    Thread.Sleep(TimeSpan.FromSeconds(10));
                }
            }
        };

        public EtlScheduler(IServiceProvider sp)
        {
            this.sp_ = sp;
        }

        public void AddProcess<EtlTask>(TimeSpan period) where EtlTask : IETLProcess
        {
            this.tasks_.Add(new EtlProcessConfig<EtlTask>(period));
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            this.workProcess_ = new Thread(this.WorkProcess);
            this.workProcess_.Start();
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            this.showtdownToken.Cancel();
            this.workProcess_.Join();
        }

        private CancellationTokenSource showtdownToken = new CancellationTokenSource();
        private Thread workProcess_;

        private void WorkProcess()
        {
            while(!showtdownToken.IsCancellationRequested)
            {
                var now = DateTime.Now;
                DateTime nextCheck = now + TimeSpan.FromMinutes(5);

                foreach(var task in this.tasks_)
                {
                    if (!task.IsRunned)
                    {
                        if(nextCheck > task.NextStart)
                        {
                            nextCheck = task.NextStart;
                        }
                    }
                }

                if(nextCheck > now)
                {
                    showtdownToken.Token.WaitHandle.WaitOne(nextCheck - now);
                }
                else
                {
                    foreach (var task in this.tasks_)
                    {
                        if (!task.IsRunned)
                        {
                            if (nextCheck == task.NextStart)
                            {
                                task.Start(this.sp_);
                            }
                        }
                    }
                }
            }

            var tasks = new List<Task>();
            foreach (var task in this.tasks_)
            {
                if (task.IsRunned)
                {
                    tasks.Add(task.Stop());
                }
            }

            Task.WaitAll(tasks.ToArray());
        }
    }
}