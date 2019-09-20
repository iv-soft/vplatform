using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Target.ModelCode.ModuleType>()
                .HasDiscriminator<string>("class")
                .HasValue<Target.ModelCode.EntityType>("entity");
        }
    }
}