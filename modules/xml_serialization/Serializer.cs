@using System.Linq;
@using System.Collections.Generic;
@{
	var entity_manager = get_service<IVySoft.VPlatform.TemplateService.Entity.IEntityManager>();
	var razor = get_service<IVySoft.VPlatform.TemplateService.Razor.IRazorManager>();
	var modules = entity_manager.get_collection<type_model.module>("modules");
	var entity_types = entity_manager.get_collection<type_model.entity_type>("entity_types");
	var entity_fields = entity_manager.get_collection<type_model.entity_field>("entity_fields");
	var entity_associations = entity_manager.get_collection<type_model.entity_association>("entity_associations");

	var module = modules.Single(x => x.Namespace == Parameters["Namespace"]);
	var entity_type = entity_types.Single(x => x.Name == Parameters["Name"] && x.Module == module.Name);
}

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;
using System.Linq;

namespace @(Parameters["Namespace"]).Xml.Serialization
{
    public class @Parameters["Name"]@((entity_type.BaseType == null) ? "" : (" : " + entity_type.BaseType))
    {
	@foreach(var field in entity_fields.Where(x => x.Module == module.Name && x.Entity == entity_type.Name))
	{
		if(string.IsNullOrWhiteSpace(field.Multiplicity) || field.Multiplicity == "1") {
		@:[XmlElement()]
	        @:public @field.Type @field.Name { get; set; }
		} else if(field.Multiplicity == "0..1") {
		@:[XmlElement()]
	        @:public @field.Type? @field.Name { get; set; }
		}
	}

	@foreach(var association in entity_associations.Where(x => x.LeftEntity == module.Namespace + "." + entity_type.Name))
	{
		@if(association.RightMultiplicity == "0..*" || association.RightMultiplicity == "1..*")
		{
			var derived_types = new Dictionary<string, string>();
			var unprocessed = new SortedSet<string>();
			var processed = new SortedSet<string>();
			unprocessed.Add(association.RightEntity);
			while(unprocessed.Count > 0)
            		{
				foreach(var base_type in unprocessed)
                		{
					unprocessed.Remove(base_type);
                    			if (!processed.Contains(base_type))
                    			{
						processed.Add(base_type);
						foreach(var derived in entity_types.Where(x => x.BaseType == base_type))
						{
							var full_name = modules.Single(m => m.Name == derived.Module).Namespace + "." + derived.Name;
                    					if (!processed.Contains(full_name) && !unprocessed.Contains(full_name))
							{
								derived_types.Add(derived.Name, modules.Single(m => m.Name == derived.Module).Namespace + ".Xml.Serialization." + derived.Name);
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
	@:public @(association.RightEntity.Substring(0, association.RightEntity.LastIndexOf('.')) + ".Xml.Serialization" + association.RightEntity.Substring(association.RightEntity.LastIndexOf('.')))[] @association.RightName { get; set; }
		}
	}
	@foreach(var association in entity_associations.Where(x => x.RightEntity == module.Namespace + "." + entity_type.Name))
	{
		@if(association.LeftMultiplicity == "0..*" || association.LeftMultiplicity == "1..*")
		{
			var derived_types = new Dictionary<string, string>();
			var unprocessed = new SortedSet<string>();
			var processed = new SortedSet<string>();
			unprocessed.Add(association.LeftEntity);
			while(unprocessed.Count > 0)
            		{
				foreach(var base_type in unprocessed)
                		{
					unprocessed.Remove(base_type);
                    			if (!processed.Contains(base_type))
                    			{
						processed.Add(base_type);
						foreach(var derived in entity_types.Where(x => x.BaseType == base_type))
						{
							var full_name = modules.Single(m => m.Name == derived.Module).Namespace + "." + derived.Name;
                    					if (!processed.Contains(full_name) && !unprocessed.Contains(full_name))
							{
								derived_types.Add(derived.Name, modules.Single(m => m.Name == derived.Module).Namespace + ".Xml.Serialization." + derived.Name);
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
	@:public @(association.LeftEntity.Substring(0, association.LeftEntity.LastIndexOf('.')) + ".Xml.Serialization" + association.LeftEntity.Substring(association.LeftEntity.LastIndexOf('.')))[] @association.LeftName { get; set; }
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

		@foreach(var field in entity_fields.Where(x => x.Module == module.Name && x.Entity == entity_type.Name))
		{
			var field_entity_type = entity_types.SingleOrDefault(x => field.Type == modules.Single(y => y.Name == x.Module).Namespace + "." + x.Name);
			if(null == field_entity_type)
			{
		@:result.@field.Name = this.@field.Name;
			}
			else
			{
		@:result.@field.Name = (@field.Type)this.@field.Name@?.ToModel();
			}
		}
		@foreach(var association in entity_associations.Where(x => x.RightEntity == module.Namespace + "." + entity_type.Name))
		{
			@if(association.LeftMultiplicity == "0..*" || association.LeftMultiplicity == "1..*")
			{
		@:result.@association.LeftName = new List<@association.LeftEntity>((this.@association.LeftName == null) ? new @association.LeftEntity@[0] : this.@association.LeftName@.Select(x => (@association.LeftEntity@)x.ToModel()));
			}
		}
		@foreach(var association in entity_associations.Where(x => x.LeftEntity == module.Namespace + "." + entity_type.Name))
		{
			@if(association.RightMultiplicity == "0..*" || association.RightMultiplicity == "1..*")
			{
		@:result.@association.RightName = new List<@association.RightEntity>((this.@association.RightName == null) ? new @association.RightEntity@[0] : this.@association.RightName@.Select(x => (@association.RightEntity@)x.ToModel()));
			}
		}
	}
}
