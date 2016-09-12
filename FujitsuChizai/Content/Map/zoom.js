var zoomRatio = 0.5;

function setZoomRatio() {
    $('#canvas').css('transform', 'scale(' + zoomRatio + ')');
}

function zoomOut() {
    zoomRatio -= 0.1;
    setZoomRatio();
}
function zoomIn() {
    zoomRatio += 0.1;
    setZoomRatio();
}