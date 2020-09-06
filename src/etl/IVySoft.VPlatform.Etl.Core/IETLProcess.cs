using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IVySoft.VPlatform.Etl.Core
{
    public interface IETLProcess
    {
        Task Process(IServiceProvider sp);

    }
}
