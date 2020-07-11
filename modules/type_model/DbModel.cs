@using System.Linq;
@using System.Collections.Generic;
@using Microsoft.Extensions.DependencyInjection;
@{
	var entity_manager = get_service<IVySoft.VPlatform.TemplateService.Entity.IEntityManager>();
	var sp = entity_manager.get_db_model<IVySoft.TypeModel.DbModel>();
	var scope = sp.CreateScope();
	var db = scope.ServiceProvider.GetService<IVySoft.TypeModel.DbModel>();
}
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;
using Microsoft.EntityFrameworkCore;

namespace @Parameters["Namespace"]
{
    public class DbModel : DbContext
    {
        public DbModel(DbContextOptions options) : base(options)
        {
        }


	@foreach(var module in db.Modules)
	{
	  	foreach(var entity_table in module.Tables)
		{
        @:public DbSet<@entity_table.EntityType> @entity_table.Name { get; set; }
		}
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
			unprocessed.Add(association.Right.Type);
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
								derived_types.Add(derived.Name, full_name);
								unprocessed.Add(full_name);
							}
						}
                    			}

					break;
				}
			}
			if(derived_types.Count > 0){
            @:modelBuilder.Entity<@association.Right.Type>()
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
			unprocessed.Add(association.Left.Type);
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
								derived_types.Add(derived.Name, full_name);
								unprocessed.Add(full_name);
							}
						}
                    			}

					break;
				}
			}
			if(derived_types.Count > 0){
            @:modelBuilder.Entity<@association.Left.Type>()
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


