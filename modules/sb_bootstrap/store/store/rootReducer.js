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

import { combineReducers } from 'redux';
@foreach(var module in db.Modules)
{
foreach(var table in module.Tables)
{
@:import { @(table.Name)Reducer } from './@(table.Name)/reducer';
}
}

const rootReducer = combineReducers({
@foreach(var module in db.Modules)
{
foreach(var table in module.Tables)
{
    @:@(table.Name)Reducer,
}
}
});
  
export default rootReducer;