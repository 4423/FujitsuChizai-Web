var registrationForm;
var registrationBinder;
var updateForm;
var updateBinder;

window.onload = function () {
    // イベントハンドラ設定
    d3.select("#svg_layer").selectAll('circle')
        .on("click", function (d, i) { circleClick($(this)); });
    d3.select("#svg_layer").selectAll('path')
        .on("click", function (d, i) { pathClick($(this)); });

    // type に応じたフォームの項目変更
    var updateFormDisplay = function ($form) {
        // 一度全て非表示に
        $form.find('#type-warp').css('display', 'none');
        $form.find('#type-light').css('display', 'none');
        $form.find('#type-place-warp').css('display', 'none');

        // 必要な項目のみ表示
        var selected = $form.find('select[name="type"] option:selected').val();
        switch (selected) {
            case "Place":
                $form.find('#type-place-warp').css('display', 'inherit');
                break;
            case "Light":
                $form.find('#type-light').css('display', 'inherit');
                break;
            case "Warp":
                $form.find('#type-warp').css('display', 'inherit');
                $form.find('#type-place-warp').css('display', 'inherit');
                break;
        }
    };

    // 登録確認画面
    var registrationConfirmDialog = function (okCallback, ngCallback) {
        config = {
            title: "Are you sure?",
            text: "This data will be registered!",
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
                okCallback();
            }
            else {
                swal("Cancelled", "This data was not registered.", "error");
                ngCallback();
            }
        });
    }

    // 更新確認画面
    var updateConfirmDialog = function (okCallback, ngCallback) {
        config = {
            title: "Are you sure?",
            text: "This data will be updated!",
            type: "warning",
            showCancelButton: true,
            confirmButtonColor: "#DD6B55",
            confirmButtonText: "Update",
            cancelButtonText: "Cancel",
            closeOnConfirm: false,
            closeOnCancel: false
        };
        swal(config, function (isConfirm) {
            if (isConfirm) {
                okCallback();
            }
            else {
                swal("Cancelled", "This data was not updated.", "error");
                ngCallback();
            }
        });
    }


    // register 関連
    registrationForm = new Form($("#form-register"), ControlMode.REGISTER);
    registrationForm.successCallback = function () {
        swal("Registered", "This data has been registered.", "success");
        $("div#register").find("#slide-3").slideUp("fast");
        $("div#register").find("#slide-2").slideUp("fast");
    };
    registrationForm.errorCallback = function () { swal("Error", "This data was not registered.", "error"); };
    registrationForm.confirmDialog = registrationConfirmDialog;
    registrationForm.updateFormDisplay = updateFormDisplay;

    registrationBinder = new Binder();
    registrationBinder.form = registrationForm;


    // update 関連
    updateForm = new Form($("#form-update"), ControlMode.UPDATE);
    updateForm.successCallback = function () {
        swal("Updated", "This data has been updated.", "success");
        $("div#update").find("#slide-2").slideUp("fast");
    };
    updateForm.errorCallback = function () { swal("Error", "This data was not updated.", "error"); };
    updateForm.confirmDialog = updateConfirmDialog;
    updateForm.updateFormDisplay = updateFormDisplay;

    updateBinder = new Binder();
    updateBinder.form = updateForm;
}


function circleClick($circle) {
    var pm = new PlaceMark($circle);
    switch (mode) {
        case ControlMode.UPDATE:
            updateBinder.placemark = pm;
            updateBinder.updateForm();
            $("div#update").find("#slide-2").slideDown("slow");
            break;

        case ControlMode.CONNECT:
            return connectPlacemark(pm);

        case ControlMode.DELETE:
            return confirmDelete(pm);
    }
}

function pathClick($path) {
    var e = new edge($path);
    switch (mode) {
        case ControlMode.DELETE: return confirmDelete(e);
    }
}


// register 関連
$(function () {
    // マーカをクリックしてtypeを指定
    $("#register-type-select li").each(function (i, elem) {
        $(elem).on("click", function () {
            selectTypeCircle($(elem).find("p").text());
        })
    });

    // マップをクリックして座標指定
    $("#svg_layer").on('click', function (e) { clickMap(e.offsetX, e.offsetY); });

    // 登録したら再びplacemark配置可能
    $("#form-register").submit(function () { $circle = pm = null; });


    var pm = null;
    var $circle = null;

    function selectTypeCircle(type) {
        if ($circle == null) {
            $circle = $(document.createElementNS("http://www.w3.org/2000/svg", "circle"));
            $circle.attr('r', 20);
            $circle.attr('floor', FLOOR);
            $circle.on("click", function (d, i) { circleClick($(this)); });

            $("div#register").find("#slide-2").slideDown("slow");
        }
        // まだmapをクリックしていない場合はtype変更可能
        if (pm == null) {
            $circle.attr('type', type);
            $circle.attr('fill', typeColor[type]);
        }
    }

    function clickMap(x, y) {
        if (mode != ControlMode.REGISTER) return;
        if ($circle == null) return;

        // 初回クリック
        if (pm == null) {
            $circle.attr('cx', x);
            $circle.attr('cy', y);
            $("#svg_layer svg").append($circle);

            pm = new PlaceMark($circle);

            $("div#register").find("#slide-3").slideDown("slow");
        }
        // 以降は座標のみ変更
        else {
            pm.x = x;
            pm.y = y;
        }

        registrationBinder.placemark = pm;
        registrationBinder.updateForm();
    }
});



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
                obj.$dom.remove();
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
        edge.$dom.on("click", function (d, i) { pathClick($(this)); });
        edgeList.push(edge);
        $('#svg_layer svg').append(edge.$dom);

        $("div#connect").find("#slide-2").slideDown();
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
                    $("div#connect").find("#slide-2").slideUp("fast");
                }
            };

            // 登録失敗定義 : 1つでも失敗したら1度だけ画面表示
            var alertAlready = false;
            errorCallback = function (edge) { 
                if (!alertAlready) {
                    alertAlready = true;
                    swal("Error", "Some of the connector was not registered.", "error");
                }
                edge.$dom.remove();
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
