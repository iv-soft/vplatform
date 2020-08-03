@{
   var table = (IVySoft.SiteBuilder.EntityTable)Parameters["Table"];
   var up_name = table.Name.ToUpper();
}
export const FETCH_@(up_name)_PENDING = 'FETCH_@(up_name)_PENDING';
export const FETCH_@(up_name)_SUCCESS = 'FETCH_@(up_name)_SUCCESS';
export const FETCH_@(up_name)_ERROR = 'FETCH_@(up_name)_ERROR';

export function fetch@(table.Name)Pending() {
    return {
        type: FETCH_@(up_name)_PENDING
    }
}

export function fetch@(table.Name)Success(data) {
    return {
        type: FETCH_@(up_name)_SUCCESS,
        result: data
    }
}

export function fetch@(table.Name)Error(error) {
    return {
        type: FETCH_@(up_name)_ERROR,
        error: error
    }
}
