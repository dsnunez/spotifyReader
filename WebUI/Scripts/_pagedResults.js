$(document).ready(function () {

    bindClicks();
    //markClickableLinks();

});

function bindClicks() {
    $(".page-control").click(function () {

        var divToUpdate = "#" + $(this).data("div");

        var page = $(this).data("page");
        var limit = $(this).data("limit");
        var query = $(this).data("search-query");
        var params = {
            page: page,
            perPage: limit,
            q: query
        };

        var control = $(this).data("control");
        var action = "Search" + $(this).data("action")+"Partial";

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