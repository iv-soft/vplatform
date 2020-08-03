@{
   var table = (IVySoft.SiteBuilder.EntityTable)Parameters["Table"];
   var up_name = table.Name.ToUpper();
}
import { fetch@(table.Name)Pending, fetch@(table.Name)Success, fetch@(table.Name)Error } from './actions';
var odatajs = require('odatajs');

export function fetch@(table.Name)() {
    return dispatch => {
        dispatch(fetch@(table.Name)Pending());
        window.odatajs.oData.read('http://localhost:5000/odata/@(table.Name)/',
            function(data) {
                dispatch(fetch@(table.Name)Success(data.value));
            },
            function(error){
                dispatch(fetchModulesError(error));
            }
        );
    }
}

export function fetch@(table.Name)ById(id) {
    return dispatch => {
        dispatch(fetch@(table.Name)Pending());
        window.odatajs.oData.read('http://localhost:5000/odata/@(table.Name)(' + id + ')',
            function(data) {
                dispatch(fetch@(table.Name)Success(data));
            },
            function(error){
                dispatch(fetchModulesError(error));
            }
        );
    }
}
