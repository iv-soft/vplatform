using Microsoft.EntityFrameworkCore;

namespace IVySoft.VPlatform.Target.ModelCode
{
    public interface IDataModel : IVySoft.VPlatform.Etl.Core.IDataModel
    {
        DbSet<Module> Modules { get; set; }


    }
}
