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
        case "delete": return confirmDelete(pm);
    }
}

function pathClick(obj) {
    var e = new edge(obj);
    switch (mode) {
        case "delete": return confirmDelete(e);
    }
}

function confirmDelete(obj) {
    config = {
        title: "Are you sure?",
        text: "You will not be able to recover this data!",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Delete",
        cancelButtonText: "Cancel",
        closeOnConfirm: false,
        closeOnCancel: false
    };
    swal(config, function (isConfirm) {
        if (isConfirm) {
            obj.delete(function () {
                $(obj.dom).remove();
                swal("Deleted", "Data has been deleted.", "success");
            }, function () {
                swal("Error", "Data was not deleted.", "error");
            });
        }
        else {
            swal("Cancelled", "Data is safe.", "error");
        }
    });
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
        $(edge.dom).on("click", function (d, i) { pathClick($(this)); });
        edgeList.push(edge);
        $('#svg_layer svg').append(edge.dom);
    }
}

var edgeList = [];
function confirmConnect() {
    config = {
        title: "Are you sure?",
        text: "Register all " + edgeList.length + " pieces of the connector.",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Register",
        cancelButtonText: "Cancel",
        closeOnConfirm: false,
        closeOnCancel: false
    };
    swal(config, function (isConfirm) {
        if (isConfirm) {
            // 登録成功定義 : 全件成功時には画面表示
            var successCount = 0;
            var total = edgeList.length;
            successCallback = function () { 
                successCount++;
                if (successCount == total) {
                    swal("Registered", "All connectors have been registered.", "success");
                }
            };

            // 登録失敗定義 : 1つでも失敗したら1度だけ画面表示
            var alertAlready = false;
            errorCallback = function (edge) { 
                if (!alertAlready) {
                    alertAlready = true;
                    swal("Error", "Some of the connector was not registered.", "error");
                }
                $(edge.dom).remove();
            };

            // 登録実行
            $.each(edgeList, function (i, edge) {
                edge.register(successCallback, errorCallback);
            });
            // 初期化
            edgeList = [];
        }
        else {
            swal("Cancelled", "All connectors were not registered.", "error");
        }
    });
}