function excecuteAction(control, action) {
    var ajaxResponse = '';
    if (entityId > 0) {
        $.ajax({
            url: '/WebApp/' + control + '/' + action + '/?id=' + entityId,
            async: false,
            type: 'get',
            success: function (data) {
                ajaxResponse = data;
            }, dataType: 'json'
        });
    }
    return ajaxResponse;
}

function excecuteAction(control, action, id) {
    var ajaxResponse = '';
    if (id > 0) {
        $.ajax({
            url: '/WebApp/' + control + '/' + action + '/?id=' + id,
            async: false,
            type: 'get',
            success: function (data) {
                ajaxResponse = data;
            },
            dataType: 'json'
        });
    }
    return ajaxResponse;
}

function excecuteAction(control, action, arrayAsString) {
    var ajaxResponse = '';
    $.ajax({
        url: '/WebApp/' + control + '/' + action,
        async: false,
        type: 'post',
        traditional: true,
        data: { idArrayAsString: arrayAsString },
        success: function (data) {
            ajaxResponse = data;
        },
        dataType: 'json'
    });
    return ajaxResponse;
}