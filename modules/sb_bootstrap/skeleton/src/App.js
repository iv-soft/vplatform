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

import React from 'react';
import './App.css';
import { Switch, Route, Redirect } from 'react-router-dom';
@foreach(var page in db.Pages)
{
@:import @page.Name from './pages/@page.Name';
}

function App() {
  return (
    <Switch>
@foreach(var page in db.Pages)
{
      <Route exact path='@page.Url' @("component={" + page.Name + "}") />
}
      <Redirect to='/' />
    </Switch>
    );
}

export default App;
