using Microsoft.EntityFrameworkCore;

namespace IVySoft.VPlatform.Target.ModelCode
{
    public interface IDataModel
    {
        DbSet<Module> Modules { get; set; }
    }
}
