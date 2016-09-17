$(function () {
    // マーカをクリックしてtypeを指定
    $("#register-type-select li").each(function (i, elem) {
        $(elem).on("click", function () {
            var type = $(elem).find("p").text().toLowerCase();
            $('#form-register [name="type"]').val(type).change();
        });
    });

    // マップをクリックして座標指定
    $("#svg_layer").on('click', function (e) {
        $('#form-register [name="x"]').val(e.offsetX);
        $('#form-register [name="y"]').val(e.offsetY);
        $('#form-register [name="floor"]').val(FLOOR);
    });
});


// 各formごとに、select変更時に入力項目が動的変更されるように設定
$(function () {
    $('form').each(function (i, form) {
        $(form).find('select[name="type"]').change(function () {
            // 一度全て非表示に
            $(form).find('#type-warp').css('display', 'none');
            $(form).find('#type-light').css('display', 'none');
            $(form).find('#type-place-warp').css('display', 'none');

            // 必要な項目のみ表示
            var selected = $(form).find('select[name="type"] option:selected').val();
            switch (selected) {
                case "place":
                    $(form).find('#type-place-warp').css('display', 'inherit');
                    break;
                case "light":
                    $(form).find('#type-light').css('display', 'inherit');
                    break;
                case "warp":
                    $(form).find('#type-warp').css('display', 'inherit');
                    $(form).find('#type-place-warp').css('display', 'inherit');
                    break;
            }
        })
    });
});

$(function () {
    $('form').submit(function (event) {
        event.preventDefault();
        var $form = $(this);
        var $button = $form.find('button');

        // 送信
        $.ajax({
            url: $form.attr('action') + $form.find('input[name="id"]').val(),
            type: $form.attr('method'),
            dataType: 'json',
            data: $form.serialize(),
            timeout: 10000,

            // 送信前
            beforeSend: function (xhr, settings) {
                // ボタンを無効化し、二重送信を防止
                $button.attr('disabled', true);
            },
            // 応答後
            complete: function (xhr, textStatus) {
                // ボタンを有効化し、再送信を許可
                $button.attr('disabled', false);
            },

            // 通信成功時の処理
            success: function (result, textStatus, xhr) {
                if (mode == "update")
                    swal("Updated", "Data has been updated.", "success");
                else if (mode == "register")
                    swal("Registered", "Data has been registered.", "success");
            },

            // 通信失敗時の処理
            error: function (xhr, textStatus, error) {
                if (mode == "update")
                    swal("Error", "Data was not updated.", "error");
                else if (mode == "register")
                    swal("Error", "Data was not registered.", "error");
            }
        });
    });
});