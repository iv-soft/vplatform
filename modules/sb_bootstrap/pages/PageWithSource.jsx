@using System.Linq;
@using System.Collections.Generic;
@using Microsoft.Extensions.DependencyInjection;
@{
  var entity_manager = get_service<IVySoft.VPlatform.TemplateService.Entity.IEntityManager>();
  var razor = get_service<IVySoft.VPlatform.TemplateService.Razor.IRazorManager>();
  var sp = entity_manager.get_db_model<IVySoft.SiteBuilder.DbModel>();
  var scope = sp.CreateScope();
  var db = scope.ServiceProvider.GetService<IVySoft.SiteBuilder.DbModel>();
  var page = db.Pages.Single(x => x.Id == (int)Parameters["PageId"]);
  var layout = db.Layouts.Single(x => x.Name == page.Layout);
  var imports = (System.Collections.Generic.Dictionary<string, string>)Parameters["Imports"];
  imports.Add("Navbar", "react-bootstrap");
  imports.Add("NavDropdown", "react-bootstrap");
  imports.Add("Nav", "react-bootstrap");

}
<div>
      <Navbar bg="light" expand="lg">
        <Navbar.Brand href="#home">@Parameters["brand"]</Navbar.Brand>
        <Navbar.Toggle aria-controls="basic-navbar-nav" />
        <Navbar.Collapse id="basic-navbar-nav">
          <Nav className="mr-auto">
@foreach(var menu_item in layout.Menu)
{
            <Nav.Link href="@menu_item.Target">@menu_item.Title</Nav.Link>
}
          </Nav>
        </Navbar.Collapse>
      </Navbar>

@foreach(var control in page.Children.OrderBy(x => x.Order))
{
	var type = control.GetType().Name;
	if(type.EndsWith("Proxy")) type = type.Substring(0, type.Length - "Proxy".Length);
	@component(type, new System.Collections.Generic.Dictionary<string, object> { ["ControlId"] = control.Id, ["Imports"] = imports })
}
</div>