using IVySoft.VPlatform.Generator.Core;
using IVySoft.VPlatform.Target.ModelCode;
using Microsoft.EntityFrameworkCore;

namespace IVySoft.VPlatform.Etl.Cmd
{
    internal class DbModel : DbContext, IDbModel, IDataModel
    {
        public DbModel(DbContextOptions options) : base(options)
        {
        }

        protected DbModel()
        {
        }

        public DbSet<Module> Modules { get; set; }
    }
}