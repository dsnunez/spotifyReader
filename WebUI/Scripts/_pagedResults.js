$(document).ready(function () {

    bindClicks();

});

function bindClicks() {
    $(".page-control").click(function () {

        var divToUpdate = "#" + $(this).data("div");

        var page = $(this).data("page");
        var limit = $(this).data("limit");
        var query = $(this).data("search");
        var params = {
            page: page,
            perPage: limit,
            q: query
        };

        var control = $(this).data("control");
        var action = $(this).data("action")+"Partial";

        $.ajax({
            url: '/' + control + '/' + action,
            async: false,
            type: 'post',
            traditional: true,
            data: params,
            success: function (data) {
                $(divToUpdate).html(data);
                bindClicks();
            },
            dataType: 'html'
        });
    });
}