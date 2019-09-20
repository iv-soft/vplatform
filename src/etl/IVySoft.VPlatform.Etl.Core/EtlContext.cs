using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IVySoft.VPlatform.Etl.Core
{
    public class EtlContext : IEtlContext
    {
        private Dictionary<Type, object> properties_ = new Dictionary<Type, object>();

        public IDataModel DataModel { get; set; }

        public T Get<T>()
        {
            return (T)this.properties_[typeof(T)];
        }

        public void Set<T>(T value)
        {
            this.properties_[typeof(T)] = value;
        }

        public void Execute(IEnumerable<IEtlStep> steps)
        {
            var processed = new HashSet<string>();
            for (; ; )
            {
                bool have_ok = false;
                bool have_failed = false;
                foreach (var processor in steps.Where(x => !processed.Contains(x.GetType().FullName)))
                {
                    bool isOk = true;
                    if (processor.Dependencies != null)
                    {
                        foreach (var dependency in processor.Dependencies)
                        {
                            if (!processed.Contains(dependency))
                            {
                                isOk = false;
                                break;
                            }
                        }
                    }

                    if (!isOk)
                    {
                        have_failed = true;
                        continue;
                    }

                    have_ok = true;
                    processed.Add(processor.GetType().FullName);
                    processor.Process(this);
                }

                if (!have_ok)
                {
                    if (!have_failed)
                    {
                        break;
                    }

                    throw new Exception("Invalid dependency schema");
                }
            }
        }
    }
}
