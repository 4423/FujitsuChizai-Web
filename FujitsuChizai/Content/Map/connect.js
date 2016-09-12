function registerPath() {
    $('path').each(function (i, e) {
        post($(e).attr('id1'), $(e).attr('id2'), $(e).attr('cost'));
    });
}

function post(id1, id2, cost) {
    jQuery.ajax({
        url: '@Request.Url.AbsoluteUri.Replace(Request.RawUrl, "")/api/edges',
        type: 'POST',
        dataType: 'json',
        data: { placeMarkId1: id1, placeMarkId2: id2, cost: cost },
        timeout: 10000,
        success: function (data) {
            alert("ok");
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert("error");
        }
    });
}