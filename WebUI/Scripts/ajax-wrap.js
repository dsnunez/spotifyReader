function excecuteAction(control, action) {
    var ajaxResponse = '';
    if (entityId != 0) {
        $.ajax({
            url: '/' + control + '/' + action,
            async: false,
            type: 'post',
            data: { id: entityId },
            success: function (data) {
                ajaxResponse = data;
            }, dataType: 'json'
        });
    }
    return ajaxResponse;
}

function excecuteAction(control, action, id) {
    var ajaxResponse = '';
    var url = '/' + control + '/' + action;
    if (id != 0) {
        $.ajax({
            url: url,
            async: false,
            type: 'post',
            data: {id:id},
            success: function (data) {
                ajaxResponse = data;
            },
            dataType: 'json'
        });
    }
    return ajaxResponse;
}

function excecuteActionMultipleParams(control, action, params) {
    excecuteActionMultipleParams(control, action, params, null, null)
}
function excecuteActionMultipleParams(control, action, params, callback, callbackParams) {
    var ajaxResponse = '';
    $.ajax({
        url: '/' + control + '/' + action,
        async: false,
        type: 'post',
        traditional: true,
        data: params,
        success: function (data) {
            ajaxResponse = data;
            if (typeof callback === "function")
                callback(callbackParams);
        },
        dataType: 'json'
    });
    return ajaxResponse;
}