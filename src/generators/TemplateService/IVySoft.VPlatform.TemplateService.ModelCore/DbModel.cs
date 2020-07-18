using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Threading.Tasks;

namespace IVySoft.VPlatform.TemplateService.ModelCore
{
    public class DbModel : DbContext
    {
        public DbModel(DbContextOptions options) : base(options)
        {
            if (!is_inited_)
            {
                lock (typeof(DbModel))
                {
                    if (!is_inited_)
                    {
                        var config = this.GetService<Microsoft.Extensions.Configuration.IConfiguration>();
                        if (config != null)
                        {
                            var section = config.GetSection("APICFG_ALLOWMIGRATION");
                            if (section.Value == "true")
                            {
                                InitDatabase(this);
                            }
                        }
                        is_inited_ = true;
                    }
                }
            }
        }


        public DbSet<IVySoft.VPlatform.TemplateService.ModelCore.Module> Modules { get; set; }

        private static bool is_inited_ = false;
        private static void InitDatabase(DbModel model)
        {
            model.Database.Migrate();
            model.AllMigrationsApplied();
        }

        private bool AllMigrationsApplied()
        {
            var applied = this.GetService<IHistoryRepository>()
                .GetAppliedMigrations()
                .Select(m => m.MigrationId);

            var total = this.GetService<IMigrationsAssembly>()
                .Migrations
                .Select(m => m.Key);

            return !total.Except(applied).Any();
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IVySoft.VPlatform.TemplateService.ModelCore.ModuleType>()
                .HasDiscriminator<string>("class")
                .HasValue<IVySoft.VPlatform.TemplateService.ModelCore.PrimitiveType>("PrimitiveType")
                .HasValue<IVySoft.VPlatform.TemplateService.ModelCore.EntityType>("EntityType")
                .HasValue<IVySoft.VPlatform.TemplateService.ModelCore.ComplexType>("ComplexType")
	        ;
        }
    }
}


