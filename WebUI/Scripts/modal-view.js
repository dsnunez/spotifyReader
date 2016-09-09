function fullscreen_modal(div_id) {
    el = document.getElementById(div_id);
    el.style.visibility = (el.style.visibility == "visible") ? "hidden" : "visible";
}
function hide_modal(div_id) {
    el = document.getElementById(div_id);
    el.style.visibility = "hidden";
}
function show_modal(div_id) {
    el = document.getElementById(div_id);
    el.style.visibility = "visible";
}