@{
   var table = (IVySoft.SiteBuilder.EntityTable)Parameters["Table"];
   var up_name = table.Name.ToUpper();
}
import {FETCH_@(up_name)_PENDING, FETCH_@(up_name)_SUCCESS, FETCH_@(up_name)_ERROR} from './actions';

const initialState = {
    pending: true,
    result: null,
    error: null
}

export function @(table.Name)Reducer(state = initialState, action) {
    switch(action.type) {
        case FETCH_@(up_name)_PENDING: 
            return {
                ...state,
                pending: true
            }
        case FETCH_@(up_name)_SUCCESS:
            return {
                ...state,
                pending: false,
		result: action.result
            }
        case FETCH_@(up_name)_ERROR:
            return {
                ...state,
                pending: false,
                error: action.error
            }
        default: 
            return state;
    }
}

