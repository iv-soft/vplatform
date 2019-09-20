using System;
using System.Collections.Generic;
using System.Text;

namespace IVySoft.VPlatform.Etl.Core
{
    public interface IEtlStep : IDependentObject
    {
        void Process(IEtlContext context);
    }
}
