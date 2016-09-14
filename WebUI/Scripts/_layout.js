$(document).ready(function () {
    $("#searchForm").submit(function (event) {
        var q = $("#searchQueryInput").val();
        q = encodeURIComponent(q);
        $("#searchQuery").val(q);
    });

    markClickableLinks();
});

function markClickableLinks()
{
    $(".clickable-link").click(function () {
        var href = $(this).data("href");
        window.location.href = href;
    });
}