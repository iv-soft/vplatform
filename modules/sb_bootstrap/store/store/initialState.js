@using System.Linq;
@using System.Collections.Generic;
@using Microsoft.Extensions.DependencyInjection;
@{
  var entity_manager = get_service<IVySoft.VPlatform.TemplateService.Entity.IEntityManager>();
  var razor = get_service<IVySoft.VPlatform.TemplateService.Razor.IRazorManager>();
  var sp = entity_manager.get_db_model<IVySoft.SiteBuilder.DbModel>();
  var scope = sp.CreateScope();
  var db = scope.ServiceProvider.GetService<IVySoft.SiteBuilder.DbModel>();
}

const initialState = {
@foreach(var module in db.Modules)
{
foreach(var table in module.Tables)
{
    @:@(table.Name)Reducer : { pending: true, result: null, error: null },
}
}
}

export default initialState;
  