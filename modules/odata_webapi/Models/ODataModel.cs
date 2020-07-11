@using System.Linq;
@using System.Collections.Generic;
@using Microsoft.Extensions.DependencyInjection;
@{
	var entity_manager = get_service<IVySoft.VPlatform.TemplateService.Entity.IEntityManager>();
	var sp = entity_manager.get_db_model<IVySoft.VPlatform.TemplateService.ModelCore.DbModel>();
	var scope = sp.CreateScope();
	var db = scope.ServiceProvider.GetService<IVySoft.VPlatform.TemplateService.ModelCore.DbModel>();
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
@foreach(var module in db.Modules)
{
  foreach(var entity_table in module.Tables)
  {
        @:builder.EntitySet<@entity_table.EntityType>("@entity_table.Name");
  }
}
            return builder.GetEdmModel();
        }
    }
}
