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
                swal("Error", "Data hasn't deleted.", "error");
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
        $('#svg_layer svg').append(edge.dom);
    }
}
