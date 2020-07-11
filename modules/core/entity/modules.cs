@using System.Linq;
@using System.Collections.Generic;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.Extensions.DependencyInjection;

@foreach(var module in Model)
{
@:namespace @module.Namespace
@:{
  @foreach(IVySoft.VPlatform.TemplateService.ModelCore.EntityType entity_type in module.Types.Where(x => x is IVySoft.VPlatform.TemplateService.ModelCore.EntityType)){
	@:public class @entity_type.Name@(string.IsNullOrWhiteSpace(entity_type.BaseType) ? "" : (" : " + entity_type.BaseType))
	@:{
		@if(entity_type.BaseType == null)
		{
	@:[Key]
	@:[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	@:public int Id { get; set; }
		}

		@foreach(var property in entity_type.Properties)
		{
			var property_type = module.Types.SingleOrDefault(x => x.Name == property.Type);
			if(null == property_type)
			{
				if(property.Multiplicity == "0..1" && property.Type != "string")
				{
		@:public @property.Type? @property.Name { get; set; } @(string.IsNullOrWhiteSpace(property.Default) ? "" : "=" + property.Default)
				}
				else
				{
		@:public @property.Type @property.Name { get; set; } @(string.IsNullOrWhiteSpace(property.Default) ? "" : "=" + property.Default)
				}
			}
			else
			{
		@:[ForeignKey(nameof(@property.Name))]
				if(property.Multiplicity == "0..1")
				{
		@:public int? @(property.Name)Id { get; set; }
				}
				else
				{
		@:public int @(property.Name)Id { get; set; }
				}
		@:public virtual @property.Type @property.Name { get; set; }
			}
		}

		@foreach(var association in module.Associations.Where(x => x.Left.Type == entity_type.Name))
		{
			@if(association.Left.Multiplicity == "0..*" || association.Left.Multiplicity == "1..*")
			{
		@:[InverseProperty(nameof(@association.Right.Type.@association.Right.Property))]
		@:public virtual IList<@association.Right.Type> @association.Left.Property { get; set; }
			}
			else
			{
		@:[ForeignKey(nameof(@association.Left.Property))]
		@:public int @(association.Left.Property)Id { get; set; }
		@:public virtual @association.Right.Type @association.Left.Property { get; set; }
			}
		}

		@foreach(var association in module.Associations.Where(x => x.Right.Type == entity_type.Name))
		{
			@if(association.Right.Multiplicity == "0..*" || association.Right.Multiplicity == "1..*")
			{
		@:[InverseProperty(nameof(@association.Left.Type.@association.Left.Property))]
		@:public virtual IList<@association.Left.Type> @association.Right.Property { get; set; }
			}
			else
			{
		@:[ForeignKey(nameof(@association.Right.Property))]
		@:public int @(association.Right.Property)Id { get; set; }
		@:public virtual @association.Left.Type @association.Right.Property { get; set; }
			}
		}
	@:}
	}

    @:public class DbModel : DbContext
    @:{
        @:public DbModel(DbContextOptions options) : base(options)
        @:{
        @:}

  	@foreach(var entity_table in module.Tables) {
        @:public DbSet<@module.Namespace.@entity_table.EntityType> @entity_table.Name { get; set; }
	}

        @:protected override void OnModelCreating(ModelBuilder modelBuilder)
        @:{
            @:base.OnModelCreating(modelBuilder);

	    @foreach(IVySoft.VPlatform.TemplateService.ModelCore.EntityType entity_type in module.Types.Where(x => x is IVySoft.VPlatform.TemplateService.ModelCore.EntityType))
	    {
		foreach(var association in module.Associations.Where(x => x.Left.Type == entity_type.Name))
		{
			if(association.Left.Multiplicity == "0..*" || association.Left.Multiplicity == "1..*")
			{
			var derived_types = new Dictionary<string, string>();
			var unprocessed = new SortedSet<string>();
			var processed = new SortedSet<string>();
			unprocessed.Add(module.Namespace + "." + association.Right.Type);
			while(unprocessed.Count > 0)
            		{
				foreach(var base_type in unprocessed)
                		{
					unprocessed.Remove(base_type);
                    			if (!processed.Contains(base_type))
                    			{
						processed.Add(base_type);
						foreach(var derived in Model.SelectMany(m => m.Types.Where(x => x.BaseType == base_type)))
						{
							var full_name = derived.Module.Namespace + "." + derived.Name;
                    					if (!processed.Contains(full_name) && !unprocessed.Contains(full_name))
							{
								derived_types.Add(derived.Name, derived.Module.Namespace + "." + derived.Name);
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
			unprocessed.Add(module.Namespace + "." + association.Left.Type);
			while(unprocessed.Count > 0)
            		{
				foreach(var base_type in unprocessed)
                		{
					unprocessed.Remove(base_type);
                    			if (!processed.Contains(base_type))
                    			{
						processed.Add(base_type);
						foreach(var derived in Model.SelectMany(m => m.Types.Where(x => x.BaseType == base_type)))
						{
							var full_name = derived.Module.Namespace + "." + derived.Name;
                    					if (!processed.Contains(full_name) && !unprocessed.Contains(full_name))
							{
								derived_types.Add(derived.Name, derived.Module.Namespace + ".Xml.Serialization." + derived.Name);
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
        @:}
	@:public static IServiceProvider CreateServiceProvider()
        @:{
	@:		var builder = new Microsoft.Extensions.Configuration.ConfigurationBuilder();
	@:		var configuration = builder.Build();
        @:
	@:		var services = new ServiceCollection();
	@:		services.AddSingleton<Microsoft.Extensions.Configuration.IConfiguration>(configuration);
        @:
	@:		services.AddEntityFrameworkInMemoryDatabase();
	@:		services.AddEntityFrameworkProxies();
	@:		services.AddDbContext<DbModel>(opt =>
	@:		{
	@:			opt.UseInMemoryDatabase("InMemoryDb");
	@:			opt.UseLazyLoadingProxies();
	@:		});
        @:
	@:		return services.BuildServiceProvider();
	@:}

    @:}


@:}

@:namespace @module.Namespace@.Xml.Serialization
@:{
  @foreach(IVySoft.VPlatform.TemplateService.ModelCore.EntityType entity_type in module.Types.Where(x => x is IVySoft.VPlatform.TemplateService.ModelCore.EntityType)){
    @:[XmlRoot(Namespace = "@module.Namespace")]
    @:public class @entity_type.Name@(string.IsNullOrWhiteSpace(entity_type.BaseType) ? "" : (" : " + entity_type.BaseType.Substring(0, entity_type.BaseType.LastIndexOf('.')) + ".Xml.Serialization" + entity_type.BaseType.Substring(entity_type.BaseType.LastIndexOf('.'))))
    @:{
		@foreach(var property in entity_type.Properties)
		{
		@:[XmlElement()]
		@:public @property.Type @property.Name { get; set; }
		}

		@foreach(var association in module.Associations.Where(x => x.Left.Type == entity_type.Name))
		{
			if(association.Left.Multiplicity == "0..*" || association.Left.Multiplicity == "1..*")
			{
			var derived_types = new Dictionary<string, string>();
			var unprocessed = new SortedSet<string>();
			var processed = new SortedSet<string>();
			unprocessed.Add(module.Namespace + "." + association.Right.Type);
			while(unprocessed.Count > 0)
            		{
				foreach(var base_type in unprocessed)
                		{
					unprocessed.Remove(base_type);
                    			if (!processed.Contains(base_type))
                    			{
						processed.Add(base_type);
						foreach(var derived in Model.SelectMany(m => m.Types.Where(x => x.BaseType == base_type)))
						{
							var full_name = derived.Module.Namespace + "." + derived.Name;
                    					if (!processed.Contains(full_name) && !unprocessed.Contains(full_name))
							{
								derived_types.Add(derived.Name, derived.Module.Namespace + ".Xml.Serialization." + derived.Name);
								unprocessed.Add(full_name);
							}
						}
                    			}

					break;
				}
			}
        @:[XmlArray()]
		@foreach(var derived in derived_types)
		{
        @:[XmlArrayItem(ElementName = "@derived.Key", Type = typeof(@(derived.Value)))]
		}
		@:public @(association.Right.Type)[] @association.Left.Property { get; set; }
			}
		}

		@foreach(var association in module.Associations.Where(x => x.Right.Type == entity_type.Name))
		{
			@if(association.Right.Multiplicity == "0..*" || association.Right.Multiplicity == "1..*")
			{
			var derived_types = new Dictionary<string, string>();
			var unprocessed = new SortedSet<string>();
			var processed = new SortedSet<string>();
			unprocessed.Add(module.Namespace + "." + association.Left.Type);
			while(unprocessed.Count > 0)
            		{
				foreach(var base_type in unprocessed)
                		{
					unprocessed.Remove(base_type);
                    			if (!processed.Contains(base_type))
                    			{
						processed.Add(base_type);
						foreach(var derived in Model.SelectMany(m => m.Types.Where(x => x.BaseType == base_type)))
						{
							var full_name = derived.Module.Namespace + "." + derived.Name;
                    					if (!processed.Contains(full_name) && !unprocessed.Contains(full_name))
							{
								derived_types.Add(derived.Name, derived.Module.Namespace + ".Xml.Serialization." + derived.Name);
								unprocessed.Add(full_name);
							}
						}
                    			}

					break;
				}
			}
        @:[XmlArray()]
		@foreach(var derived in derived_types)
		{
        @:[XmlArrayItem(ElementName = "@derived.Key", Type = typeof(@(derived.Value)))]
		}
		@:public @(association.Left.Type)[] @association.Right.Property { get; set; }
			}
		}

		@:public @((entity_type.BaseType == null) ? "virtual" : "override") object ToModel()
		@:{
			@:var result = new @module.Namespace.@(entity_type.Name)();
			@:this.InitModel(result);
			@:return result;
		@:}

		@:protected void InitModel(@module.Namespace.@entity_type.Name result)
		@:{
			@if(entity_type.BaseType != null)
			{
			@:base.InitModel(result);
			}

			@foreach(var property in entity_type.Properties)
			{
				var property_type = module.Types.SingleOrDefault(x => x.Name == property.Type);
				if(null == property_type)
				{
				@:result.@property.Name = this.@property.Name;
				}
				else
				{
				@:result.@property.Name = (@module.Namespace.@property.Type)this.@property.Name@?.ToModel();
				}
			}
		@foreach(var association in module.Associations.Where(x => x.Left.Type == entity_type.Name))
		{
			@if(association.Left.Multiplicity == "0..*" || association.Left.Multiplicity == "1..*")
			{
		@:result.@association.Left.Property = new List<@module.Namespace.@association.Right.Type>((this.@association.Left.Property == null) ? new @module.Namespace.@association.Right.Type@[0] : this.@association.Left.Property@.Select(x => (@(module.Namespace).@association.Right.Type@)x.ToModel()));
			}
		}
		@foreach(var association in module.Associations.Where(x => x.Right.Type == entity_type.Name))
		{
			@if(association.Right.Multiplicity == "0..*" || association.Right.Multiplicity == "1..*")
			{
		@:result.@association.Right.Property = new List<@module.Namespace.@association.Left.Type>((this.@association.Right.Property == null) ? new @module.Namespace.@association.Left.Type@[0] : this.@association.Right.Property@.Select(x => (@(module.Namespace).@association.Left.Type@)x.ToModel()));
			}
		}
		@:}
	@:}
	}
@:}

}

