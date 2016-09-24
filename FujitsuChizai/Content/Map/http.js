var Http = {
    asyncPost: function (url, data, successCallback, errorCallback) {
        Http.asyncCore(url, "POST", data, successCallback, errorCallback);
    },

    asyncPut: function (url, data, successCallback, errorCallback) {
        Http.asyncCore(url, "PUT", data, successCallback, errorCallback);
    },

    asyncDelete: function (url, data, successCallback, errorCallback) {
        Http.asyncCore(url, "DELETE", data, successCallback, errorCallback);
    },

    asyncCore: function (url, type, data, successCallback, errorCallback) {
        $.ajax({
            url: url,
            type: type,
            dataType: 'json',
            data: data,
            timeout: 10000,
            success: successCallback,
            error: errorCallback
        });
    }
}