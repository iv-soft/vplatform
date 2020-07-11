@using System.Linq;
@using System.Collections.Generic;
@using Microsoft.Extensions.DependencyInjection;
@{
	var entity_manager = get_service<IVySoft.VPlatform.TemplateService.Entity.IEntityManager>();
	var sp = entity_manager.get_db_model<IVySoft.VPlatform.TemplateService.ModelCore.DbModel>();
	var scope = sp.CreateScope();
	var db = scope.ServiceProvider.GetService<IVySoft.VPlatform.TemplateService.ModelCore.DbModel>();
}
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

namespace @Parameters["Namespace"]
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


	@foreach(var module in db.Modules)
	{
	  	foreach(var entity_table in module.Tables)
		{
        @:public DbSet<@entity_table.EntityType> @entity_table.Name { get; set; }
		}
	}

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

	@foreach(var module in db.Modules)
	{
	    foreach(var association in module.Associations)
	    {
		if(association.Left.Multiplicity == "0..*" || association.Left.Multiplicity == "1..*")
		{
			var derived_types = new Dictionary<string, string>();
			var unprocessed = new SortedSet<string>();
			var processed = new SortedSet<string>();
			var type_name = association.Right.Type;
			for(;;)
			{
				var type = db.Modules.SelectMany(m => m.Types.Where(x => m.Namespace + "." + x.Name == type_name)).Single();
				if(type.BaseType == null)
				{
					break;
				}

				type_name = type.BaseType;
			}
			unprocessed.Add(type_name);

			while(unprocessed.Count > 0)
            		{
				foreach(var base_type in unprocessed)
                		{
					unprocessed.Remove(base_type);
                    			if (!processed.Contains(base_type))
                    			{
						processed.Add(base_type);
						foreach(var derived in db.Modules.SelectMany(m => m.Types.Where(x => x.BaseType == base_type)))
						{
							var full_name = derived.Module.Namespace + "." + derived.Name;
                    					if (!processed.Contains(full_name) && !unprocessed.Contains(full_name))
							{
								if((derived is IVySoft.VPlatform.TemplateService.ModelCore.EntityType) && !((IVySoft.VPlatform.TemplateService.ModelCore.EntityType)derived).Abstract)
								{
									derived_types.Add(derived.Name, full_name);
								}
								unprocessed.Add(full_name);
							}
						}
                    			}

					break;
				}
			}
			if(derived_types.Count > 0){
            @:modelBuilder.Entity<@type_name>()
            @:    .HasDiscriminator<string>("class")
		@foreach(var derived in derived_types)
		{
            @:    .HasValue<@(derived.Value)>("@derived.Key")
		}
	    @:    ;
			}
			}

			if(association.Right.Multiplicity == "0..*" || association.Right.Multiplicity == "1..*")
			{
			var derived_types = new Dictionary<string, string>();
			var unprocessed = new SortedSet<string>();
			var processed = new SortedSet<string>();
			var type_name = association.Left.Type;
			for(;;)
			{
				var type = db.Modules.SelectMany(m => m.Types.Where(x => m.Namespace + "." + x.Name == type_name)).Single();
				if(type.BaseType == null)
				{
					break;
				}

				type_name = type.BaseType;
			}
			unprocessed.Add(type_name);
			while(unprocessed.Count > 0)
            		{
				foreach(var base_type in unprocessed)
                		{
					unprocessed.Remove(base_type);
                    			if (!processed.Contains(base_type))
                    			{
						processed.Add(base_type);
						foreach(var derived in db.Modules.SelectMany(m => m.Types.Where(x => x.BaseType == base_type)))
						{
							var full_name = derived.Module.Namespace + "." + derived.Name;
                    					if (!processed.Contains(full_name) && !unprocessed.Contains(full_name))
							{
								if((derived is IVySoft.VPlatform.TemplateService.ModelCore.EntityType) && !((IVySoft.VPlatform.TemplateService.ModelCore.EntityType)derived).Abstract)
								{
									derived_types.Add(derived.Name, full_name);
								}
								unprocessed.Add(full_name);
							}
						}
                    			}

					break;
				}
			}
			if(derived_types.Count > 0){
            @:modelBuilder.Entity<@type_name>()
            @:    .HasDiscriminator<string>("class")
		@foreach(var derived in derived_types)
		{
            @:    .HasValue<@(derived.Value)>("@derived.Key")
		}
	    @:;
			}
			}
		}
	    }
        }
    }
}


