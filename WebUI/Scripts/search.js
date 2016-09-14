$(document).ready(function () {
    $("#searchForm").submit(function (event) {
        var q = $("#searchQueryInput").val();
        q = encodeURIComponent(q);
        $("#searchQuery").val(q);
    });
});