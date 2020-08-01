@using System.Linq;
@using System.Collections.Generic;
@using Microsoft.Extensions.DependencyInjection;
@{
  var bindings = (IEnumerable<IVySoft.SiteBuilder.Binding>)Parameters["Bindings"];
}

@foreach(var binding in bindings)
{
    switch(binding.SourceType)
    {
    case  "odata":
    {
    @:odatajs.oData.read(serviceRoot + @binding.Source,
    @:function(data){
	    foreach(var field in binding.Fields)
	    {
            @:bind_@(field.Target)(data["@field.Value"]);
            }
    @:});
	    break;
    }
    case  "odata_collection":
    {
    @:odatajs.oData.read(serviceRoot + @binding.Source,
    @:function(data){
        @:data.value.forEach(element => {
	    foreach(var field in binding.Fields)
	    {
            @:bind_@(field.Target)(element["@field.Value"]);
            }
        @:});
    @:});
	    break;
    }
    case "property":
    {
	    foreach(var field in binding.Fields)
	    {
    @:bind_@(field.Target)(element["@field.Value"]);
            }
	break;
    }
    default:
    {
	throw new System.Exception("Invalid binding sourceType " + binding.SourceType);
    }
    }
}