@using System.Linq;
@{
	var entity_manager = get_service<IVySoft.VPlatform.TemplateService.Entity.IEntityManager>();
	var razor = get_service<IVySoft.VPlatform.TemplateService.Razor.IRazorManager>();
	var modules = entity_manager.get_model<type_model.module>();
	var entity_types = entity_manager.get_collection<type_model.entity_type>("entity_types");
	var entity_fields = entity_manager.get_collection<type_model.entity_field>("entity_fields");
	var entity_associations = entity_manager.get_collection<type_model.entity_association>("entity_associations");

	var module = modules.Single(x => x.Namespace == Parameters["Namespace"]);
	var entity_type = entity_types.Single(x => x.Name == Parameters["Name"] && x.Module == module.Name);
}
using System;
using System.ComponentModel.DataAnnotations;

namespace @Parameters["Namespace"]
{
    public class @Parameters["Name"]@((entity_type.BaseType == null) ? "" : (" : " + entity_type.BaseType))
    {
        [Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

	@foreach(var field in entity_fields.Where(x => x.Module == module.Name && x.Entity == entity_type.Name))
	{
		var field_entity_type = entity_types.SingleOrDefault(x => field.Type == modules.Single(y => y.Name == x.Module).Namespace + "." + x.Name);
		if(null == field_entity_type) {
			if(string.IsNullOrWhiteSpace(field.Multiplicity) || field.Multiplicity == "1") {
	        @:public @field.Type @field.Name { get; set; }
			} else if(field.Multiplicity == "0..1") {
	        @:public @field.Type? @field.Name { get; set; }
			}
		} else {
			if(string.IsNullOrWhiteSpace(field.Multiplicity) || field.Multiplicity == "1") {
		@:[ForeignKey(nameof(@field.Name))]
		@:public int @(field.Name)Id { get; set; }
	        @:public @field.Type @field.Name { get; set; }
			} else if(field.Multiplicity == "0..1") {
		@:[ForeignKey(nameof(@field.Name))]
		@:public int? @(field.Name)Id { get; set; }
	        @:public @field.Type @field.Name { get; set; }
			}
		}
	}

	@foreach(var association in entity_associations.Where(x => x.LeftEntity == module.Namespace + "." + entity_type.Name))
	{
		@if(association.RightMultiplicity == "0..*" || association.RightMultiplicity == "1..*")
		{
		@:[InverseProperty(nameof(@association.RightEntity.@association.LeftName))]
		@:public virtual IList<@association.RightEntity> @association.RightName { get; set; }
		}
		else
		{
		@:[ForeignKey(nameof(@association.RightName))]
		@:public int @(association.RightName)Id { get; set; }
		@:public virtual @association.RightEntity @association.RightName { get; set; }
		}
	}
	@foreach(var association in entity_associations.Where(x => x.RightEntity == module.Namespace + "." + entity_type.Name))
	{
		@if(association.LeftMultiplicity == "0..*" || association.LeftMultiplicity == "1..*")
		{
		@:[InverseProperty(nameof(@association.LeftEntity.@association.RightName))]
		@:public virtual IList<@association.LeftEntity> @association.LeftName { get; set; }
		}
		else
		{
		@:[ForeignKey(nameof(@association.LeftName))]
		@:public int @(association.LeftName)Id { get; set; }
		@:public virtual @association.LeftEntity @association.LeftName { get; set; }
		}
	}
    }
}
