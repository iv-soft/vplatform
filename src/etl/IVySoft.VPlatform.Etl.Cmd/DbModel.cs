using Microsoft.EntityFrameworkCore;

namespace IVySoft.VPlatform.Etl.Cmd
{
    internal class DbModel : DbContext, Target.ModelCode.IDataModel
    {
        public DbModel(DbContextOptions options) : base(options)
        {
        }

        protected DbModel()
        {
        }

        public DbSet<Target.ModelCode.Module> Modules { get; set; }

        void Core.IDataModel.SaveChanges()
        {
            ((DbContext)this).SaveChanges();
        }
    }
}