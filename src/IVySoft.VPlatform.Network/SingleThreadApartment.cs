using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace IVySoft.VPlatform.Network
{
    public class SingleThreadApartment
    {
        private Queue<Func<Task>> actions_ = new Queue<Func<Task>>();
        private Thread work_thread_;
        private Exception failed_state_;

        public void invoke(Func<Task> action)
        {
            lock (this)
            {
                if (null != this.failed_state_)
                {
                    throw this.failed_state_;
                }

                bool need_to_run = (this.actions_.Count == 0);
                this.actions_.Enqueue(action);

                if (need_to_run)
                {
                    this.work_thread_ = new Thread(this.process_actions);
                    this.work_thread_.Start();
                }
            }
        }

        public void join()
        {
            Thread work_thread;
            lock (this)
            {
                work_thread = this.work_thread_;
                if (null == work_thread)
                {
                    return;
                }
            }

            work_thread.Join();

        }

        private void process_actions()
        {
            try
            {
                for (; ; )
                {
                    Func<Task> action;
                    lock (this)
                    {
                        action = this.actions_.Peek();
                    }

                    action().Wait();
                    lock (this)
                    {
                        this.actions_.Dequeue();
                        if (this.actions_.Count == 0)
                        {
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.failed_state_ = ex;
            }
        }
    }
}

