@using System.Linq;
@{
	var razor = get_service<IVySoft.VPlatform.TemplateService.Razor.IRazorManager>();
	var entity_manager = get_service<IVySoft.VPlatform.TemplateService.Entity.IEntityManager>();
	var modules = entity_manager.get_collection<type_model.module>("modules");
	var entity_tables = entity_manager.get_collection<type_model.entity_table>("entity_tables");
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace @Parameters["Namespace"]
{
    public class DbModel : DbContext
    {
        public DbModel(DbContextOptions<DbModel> options)
            : base(options)
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

@foreach(var entity_table in entity_tables)
{
        @:public DbSet<@(modules.Single(x => x.Name == entity_table.Module).Namespace + "." + entity_table.Type)> @entity_table.Name { get; set; }
}

        private static bool is_inited_ = false;
        private static void InitDatabase(DbModel model)
        {
            model.Database.Migrate();
            if (model.AllMigrationsApplied())
            {
                model.EnsureSeedData();
            }
        }

        private void EnsureSeedData()
        {
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
    }
}
