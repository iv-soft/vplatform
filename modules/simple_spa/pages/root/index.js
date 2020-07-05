@{
 var entity_manager = get_service<IVySoft.VPlatform.TemplateService.Entity.IEntityManager>();
 var pages = entity_manager.get_collection<simple_spa.page>("pages");
}

import $ from 'jquery';
import 'jqueryrouter';

function loadCSS(href) {
     var cssLink = $("<link rel='stylesheet' type='text/css' href='"+href+"'>");
     $("head").append(cssLink); 
}

function _loadPage(page) {
    var url = '/pages/' + page + '/index.html';
 
    $.get(url, function(html) {
	loadCSS('/pages/' + page + '/index.css');
        $('#app').html(html);
	$.getScript('/pages/' + page + '/bundle.js');
    });
}

window.addEventListener('load', () => {
   if('/' === window.location.pathname){
    	_loadPage('index');
   }
   @foreach(var page in pages)
   {
   @:else if('/@page.path' === window.location.pathname || '/@page.path/' === window.location.pathname){
    	@:_loadPage('@page.path');
   @:}
   }
}); 
