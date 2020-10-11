@using System.Linq;
@using System.Collections.Generic;
@using Microsoft.Extensions.DependencyInjection;
@{
	var entity_manager = get_service<IVySoft.VPlatform.TemplateService.Entity.IEntityManager>();
	var razor = get_service<IVySoft.VPlatform.TemplateService.Razor.IRazorManager>();
	var sp = entity_manager.get_db_model<IVySoft.VPlatform.TemplateService.ModelCore.DbModel>();
	var scope = sp.CreateScope();
	var db = scope.ServiceProvider.GetService<IVySoft.VPlatform.TemplateService.ModelCore.DbModel>();
	var module = db.Modules.Single(x => x.Namespace == Parameters["Namespace"]);
	var entity_type = (IVySoft.VPlatform.TemplateService.ModelCore.EntityType)module.Types.Single(x => x.Name == Parameters["Name"]);
	var entity_associations = module.Associations;
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace @Parameters["Namespace"]
{
    public @(entity_type.Abstract ? "abstract " : "")class @Parameters["Name"]@((entity_type.BaseType == null) ? "" : (" : " + entity_type.BaseType))
    {
	@if(entity_type.BaseType == null)
	{
        @:[Key]
	@:[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        @:public int Id { get; set; }
        }

	@foreach(var field in entity_type.Properties)
	{
		var field_entity_type = module.Types.SingleOrDefault(x => field.Type == module.Namespace + "." + x.Name);

		if(null == field_entity_type || !(field_entity_type is IVySoft.VPlatform.TemplateService.ModelCore.EntityType)) {
			if(field_entity_type != null || string.IsNullOrWhiteSpace(field.Multiplicity) || field.Multiplicity == "1") {
	        @:public @((field_entity_type != null) ? "virtual " : "")@field.Type @field.Name { get; set; }
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

	@foreach(var association in entity_associations.Where(x => x.Left.Type == module.Namespace + "." + entity_type.Name))
	{
		@if(association.Right.Multiplicity == "0..*" || association.Right.Multiplicity == "1..*")
		{
		@:[InverseProperty(nameof(@association.Right.Type.@association.Left.Property))]
		@:public virtual IList<@association.Right.Type> @association.Right.Property { get; set; }
		}
		else if(string.IsNullOrWhiteSpace(association.Right.Multiplicity) || association.Right.Multiplicity == "1")
		{
		@:[ForeignKey(nameof(@association.Right.Property))]
		@:public int @(association.Right.Property)Id { get; set; }
		@:public virtual @association.Right.Type @association.Right.Property { get; set; }
		}
		else
		{
		@:[ForeignKey(nameof(@association.Right.Property))]
		@:public int? @(association.Right.Property)Id { get; set; }
		@:public virtual @association.Right.Type @association.Right.Property { get; set; }
		}
	}
	@foreach(var association in entity_associations.Where(x => x.Right.Type == module.Namespace + "." + entity_type.Name))
	{
		@if(association.Left.Multiplicity == "0..*" || association.Left.Multiplicity == "1..*")
		{
		@:[InverseProperty(nameof(@association.Left.Type.@association.Right.Property))]
		@:public virtual IList<@association.Left.Type> @association.Left.Property { get; set; }
		}
		else if(string.IsNullOrWhiteSpace(association.Left.Multiplicity) || association.Left.Multiplicity == "1")
		{
		@:[ForeignKey(nameof(@association.Left.Property))]
		@:public int @(association.Left.Property)Id { get; set; }
		@:public virtual @association.Left.Type @association.Left.Property { get; set; }
		}
		else
		{
		@:[ForeignKey(nameof(@association.Left.Property))]
		@:public int? @(association.Left.Property)Id { get; set; }
		@:public virtual @association.Left.Type @association.Left.Property { get; set; }
		}
	}
    }
}
