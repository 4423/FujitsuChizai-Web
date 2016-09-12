var pathSvg;
var circleSvg;

window.onload = function () {
    pathSvg = d3.select("#path_canvas svg");
    circleSvg = d3.select("#circle_canvas svg");

    // イベントハンドラ設定
    d3.select("#circle_canvas").selectAll('circle')
        .on("click", function (d, i) { mouseclick($(this)); });
}

var isConnect = false;
var startNode;
function mouseclick(node) {
    if (!isConnect) {
        isConnect = true;
        startNode = node;
    }
    else {
        if (startNode.attr('id') == node.attr('id')) {
            return; // 自身同士は接続させない
        }
        isConnect = false;
        drawConnection(startNode, node);
    }
}

// 属性から[x,y]配列を取得
function getPoint(obj) {
    return [obj.attr('cx'), obj.attr('cy')];
}

// 2点間の距離を1/10した値(切り上げ)
function getCost(pos) {
    return Math.ceil(0.1 * Math.sqrt(
        Math.pow(Math.abs(pos[0][0] - pos[1][0]), 2) //x^2
        + Math.pow(Math.abs(pos[0][1] - pos[1][1]), 2) //y^2
    ));
}

// 照明同士かどうか
function isLightEach(obj1, obj2) {
    return obj1.attr('type') == "Light" && obj2.attr('type') == "Light";
}

// 線を描画
function drawConnection(node1, node2) {
    var line = d3.svg.line()
        .x(function (d) { return d[0]; })
        .y(function (d) { return d[1]; });
    var pos = [getPoint(node1), getPoint(node2)];
    pathSvg.append("path")
        .attr({
            'd': line(pos),
            'stroke': 'gray',
            'stroke-width': 5,
            'id1': node1.attr('id'),
            'id2': node2.attr('id'),
            // 照明-場所などのコストは0
            'cost': isLightEach(node1, node2) ? getCost(pos) : 0
        });
}