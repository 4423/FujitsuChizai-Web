window.onload = function () {
    // イベントハンドラ設定
    d3.select("#svg_layer").selectAll('circle')
        .on("click", function (d, i) { circleClick($(this)); });
    d3.select("#svg_layer").selectAll('path')
        .on("click", function (d, i) { pathClick($(this)); });
}


function circleClick(obj) {
    var pm = new placemark(obj);
    switch (mode) {
        case "register": return pm.register();
        case "update": return pm.update();
        case "connect": return connectPlacemark(pm);
        case "delete": return pm.delete(function () { $(pm.dom).remove(); });
    }
}

function pathClick(obj) {
    var e = new edge(obj);
    switch (mode) {
        case "delete": return e.delete(function () { $(e.dom).remove();});
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
        $('#svg_layer svg').append(edge.dom);
    }
}
