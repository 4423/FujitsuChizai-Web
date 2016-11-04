$(function () {
    var zoomRatio = 0.5;

    function setZoomRatio() {
        $('#canvas').css('transform', 'scale(' + zoomRatio + ')');
    }

    $("#zoomout").click(function () {
        zoomRatio -= 0.1;
        setZoomRatio();
    });
    $("#zoomin").click(function () {
        zoomRatio += 0.1;
        setZoomRatio();
    });
});