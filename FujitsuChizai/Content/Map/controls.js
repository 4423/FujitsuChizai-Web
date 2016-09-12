$(function () {
    $(".controls a").click(function () {
        var mode = $(this).attr("id");
        $(".slide_box#"+ mode).slideToggle("fast");
    });
});