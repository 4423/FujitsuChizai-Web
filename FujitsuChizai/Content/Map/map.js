var pathSvg;
var circleSvg;

window.onload = function () {
    pathSvg = d3.select("#path_canvas svg");
    circleSvg = d3.select("#circle_canvas svg");

    // イベントハンドラ設定
    d3.select("#circle_canvas").selectAll('circle')
        .on("click", function (d, i) { circleClick($(this)); });
    d3.select("#path_canvas").selectAll('path')
        .on("click", function (d, i) { pathClick($(this)); });
}


function circleClick(obj) {
    var pm = new placemark(obj);
    switch (mode) {
        case "register": return pm.register();
        case "update": return pm.update();
        case "connect": return connectPlacemark(pm);
        case "delete": return pm.delete();
    }
}

function pathClick(obj) {
    switch (mode) {
        case "delete": deletePlaceMark(obj); break;
    }
}

var isConnect = false;
var firstPm;
function connectPlacemark(pm) {
    if (!isConnect) {
        isConnect = true;
        firstPm = pm;
    }
    else if (firstPm.id != pm.id) { // 自身同士は接続させない
        isConnect = false;
        var edge = firstPm.connect(pm);
        $('#path_canvas svg').append(edge.dom);
    }
}
