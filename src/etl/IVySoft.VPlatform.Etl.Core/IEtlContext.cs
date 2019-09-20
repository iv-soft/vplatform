using System;
using System.Collections.Generic;
using System.Text;

namespace IVySoft.VPlatform.Etl.Core
{
    public interface IEtlContext
    {
        IDataModel DataModel { get; }
        T Get<T>();
        void Set<T>(T value);
    }
}
