$(document).ready(function () {
    $(".view-tracks").click(function () {
        $(this).text($(this).text() == "Ocultar canciones"? "Ver canciones":"Ocultar canciones");
        var id = $(this).data("id");
        $("#" + id).toggle();
    });
});