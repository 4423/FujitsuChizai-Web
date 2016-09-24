var ControlMode = {
    NONE: "none",
    REGISTER: "register",
    UPDATE: "update",
    CONNECT: "connect",
    DELETE: "delete"
};

// いま展開されているスライドボックスの種類
var mode = ControlMode.NONE;

$(function () {
    $(".controls a").click(function () {
        // 閉じる
        if (mode != ControlMode.NONE) {
            $(".slide_box#" + mode).slideToggle("fast");
            // 閉じて別のを開く
            currMode = $(this).attr("id");
            if (mode != currMode) {
                mode = currMode;
                $(".slide_box#" + mode).slideToggle("fast");
            }
            // 閉じるだけ
            else {
                mode = ControlMode.NONE;
            }
        }
        // 開ける
        else {
            mode = $(this).attr("id");
            $(".slide_box#" + mode).slideToggle("fast");
        }
    });
});
