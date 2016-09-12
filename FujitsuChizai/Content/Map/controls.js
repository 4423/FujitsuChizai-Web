var mode = null;

$(function () {
    $(".controls a").click(function () {
        // 閉じる
        if (mode != null) {
            $(".slide_box#" + mode).slideToggle("fast");
            // 閉じて別のを開く
            currMode = $(this).attr("id");
            if (mode != currMode) {
                mode = currMode;
                $(".slide_box#" + mode).slideToggle("fast");
            }
            // 閉じるだけ
            else {
                mode = null;
            }
        }
        // 開ける
        else {
            mode = $(this).attr("id");
            $(".slide_box#" + mode).slideToggle("fast");
        }
    });
});