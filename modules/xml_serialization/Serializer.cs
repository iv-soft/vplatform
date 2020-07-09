@using System.Linq;
@using System.Collections.Generic;
@using Microsoft.Extensions.DependencyInjection;
@{
	var entity_manager = get_service<IVySoft.VPlatform.TemplateService.Entity.IEntityManager>();
	var razor = get_service<IVySoft.VPlatform.TemplateService.Razor.IRazorManager>();
	var sp = entity_manager.get_db_model<IVySoft.TypeModel.DbModel>();
	var scope = sp.CreateScope();
	var db = scope.ServiceProvider.GetService<IVySoft.TypeModel.DbModel>();
	var module = db.Modules.Single(x => x.Namespace == Parameters["Namespace"]);
	var entity_type = (IVySoft.TypeModel.EntityType)module.Types.Single(x => x.Name == Parameters["Name"]);
	var entity_associations = module.Associations;

}

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;
using System.Linq;

namespace @(Parameters["Namespace"]).Xml.Serialization
{
    [XmlRoot(Namespace("@Parameters["Namespace"]"))]
    public class @Parameters["Name"]@((entity_type.BaseType == null) ? "" : (" : " + entity_type.BaseType))
    {
	@foreach(var field in entity_type.Properties)
	{
		if(string.IsNullOrWhiteSpace(field.Multiplicity) || field.Multiplicity == "1") {
		@:[XmlElement()]
	        @:public @field.Type @field.Name { get; set; }
		} else if(field.Multiplicity == "0..1") {
		@:[XmlElement()]
	        @:public @field.Type? @field.Name { get; set; }
		}
	}

	@foreach(var association in entity_associations.Where(x => x.Left.Type == entity_type.Name))
	{
		@if(association.Right.Multiplicity == "0..*" || association.Right.Multiplicity == "1..*")
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
						foreach(IVySoft.TypeModel.EntityType derived in module.Types.Where(x => x is IVySoft.TypeModel.EntityType))
						{
						   if(derived.BaseType == base_type)
						   {
							var full_name = module.Namespace + "." + derived.Name;
                    					if (!processed.Contains(full_name) && !unprocessed.Contains(full_name))
							{
								derived_types.Add(derived.Name, module.Namespace + ".Xml.Serialization." + derived.Name);
								unprocessed.Add(full_name);
							}
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
	@:public @(association.Right.Type.Substring(0, association.Right.Type.LastIndexOf('.')) + ".Xml.Serialization" + association.Right.Type.Substring(association.Right.Type.LastIndexOf('.')))[] @association.Right.Property { get; set; }
		}
	}
	@foreach(var association in entity_associations.Where(x => x.Right.Type == module.Namespace + "." + entity_type.Name))
	{
		@if(association.Left.Multiplicity == "0..*" || association.Left.Multiplicity == "1..*")
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
						foreach(IVySoft.TypeModel.EntityType derived in module.Types.Where(x => x is IVySoft.TypeModel.EntityType))
						{
						   if(derived.BaseType == base_type)
						   {
							var full_name = module.Namespace + "." + derived.Name;
                    					if (!processed.Contains(full_name) && !unprocessed.Contains(full_name))
							{
								derived_types.Add(derived.Name, module.Namespace + ".Xml.Serialization." + derived.Name);
								unprocessed.Add(full_name);
							}
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
	@:public @(association.Left.Type.Substring(0, association.Left.Type.LastIndexOf('.')) + ".Xml.Serialization" + association.Left.Type.Substring(association.Left.Type.LastIndexOf('.')))[] @association.Left.Property { get; set; }
		}
	}
	
	public @((entity_type.BaseType == null) ? "virtual" : "override") object ToModel()
	{
		var result = new @Parameters["Namespace"].@(entity_type.Name)();
		this.InitModel(result);
		return result;
	}

	protected void InitModel(@Parameters["Namespace"].@(entity_type.Name) result)
	{
		@if(entity_type.BaseType != null)
		{
		@:base.InitModel(result);
		}

		@foreach(var field in entity_type.Properties)
		{
			var field_entity_type = module.Types.SingleOrDefault(x => field.Type == module.Namespace + "." + x.Name);
			if(null == field_entity_type)
			{
		@:result.@field.Name = this.@field.Name;
			}
			else
			{
		@:result.@field.Name = (@field.Type)this.@field.Name@?.ToModel();
			}
		}
		@foreach(var association in entity_associations.Where(x => x.Right.Type == module.Namespace + "." + entity_type.Name))
		{
			@if(association.Left.Multiplicity == "0..*" || association.Left.Multiplicity == "1..*")
			{
		@:result.@association.Left.Property = new List<@association.Left.Type>((this.@association.Left.Property == null) ? new @association.Left.Type@[0] : this.@association.Left.Property@.Select(x => (@association.Left.Type@)x.ToModel()));
			}
		}
		@foreach(var association in entity_associations.Where(x => x.Left.Type == module.Namespace + "." + entity_type.Name))
		{
			@if(association.Right.Multiplicity == "0..*" || association.Right.Multiplicity == "1..*")
			{
		@:result.@association.Right.Property = new List<@association.Right.Type>((this.@association.Right.Property == null) ? new @association.Right.Type@[0] : this.@association.Right.Property@.Select(x => (@association.Right.Type@)x.ToModel()));
			}
		}
	}
    }
}
