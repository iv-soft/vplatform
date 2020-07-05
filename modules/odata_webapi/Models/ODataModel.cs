@using System.Linq;
@{
	var razor = get_service<IVySoft.VPlatform.TemplateService.Razor.IRazorManager>();
	var entity_manager = get_service<IVySoft.VPlatform.TemplateService.Entity.IEntityManager>();
	var modules = entity_manager.get_collection<type_model.module>("modules");
	var entity_tables = entity_manager.get_collection<type_model.entity_table>("entity_tables");
}

using IVySoft.SiteBuilder.FieldType;
using Microsoft.AspNet.OData.Builder;
using Microsoft.OData.Edm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace @Parameters["Namespace"]
{
    internal class ODataModel
    {
        public static IEdmModel GetEdmModel()
        {
            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
@foreach(var entity_table in entity_tables)
{
        @:builder.EntitySet<@(modules.Single(x => x.Name == entity_table.Module).Namespace + "." + entity_table.Type)>("@entity_table.Name");
}
            return builder.GetEdmModel();
        }
    }
}
