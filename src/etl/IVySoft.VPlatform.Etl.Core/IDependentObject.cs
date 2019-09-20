using System;

namespace IVySoft.VPlatform.Etl.Core
{
    public interface IDependentObject
    {
        string[] Dependencies { get; }
    }
}
