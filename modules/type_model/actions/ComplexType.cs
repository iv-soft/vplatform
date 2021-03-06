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
	var entity_type = (IVySoft.VPlatform.TemplateService.ModelCore.ComplexType)module.Types.Single(x => x.Name == Parameters["Name"]);
	var entity_associations = module.Associations;
}
using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace @Parameters["Namespace"]
{
    [Owned]
    public class @Parameters["Name"]@((entity_type.BaseType == null) ? "" : (" : " + entity_type.BaseType))
    {
	@foreach(var field in entity_type.Properties)
	{
		var field_entity_type = module.Types.SingleOrDefault(x => field.Type == module.Namespace + "." + x.Name);

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
    }
}
