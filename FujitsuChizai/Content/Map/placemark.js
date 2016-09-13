var placemark = (function () {
    var ENDPOINT = "https://fujitsu-chizai.azurewebsites.net/api/places/";
    if (location.hostname == "localhost") {
        ENDPOINT = "http://localhost:11943/api/places/";
    }

    // constructor
    var placemark = function (circle) {
        this.dom = circle;
        this.id = circle.attr("id");
        this.x = circle.attr("cx");
        this.y = circle.attr("cy");
        this.floor = circle.attr("floor");        
        this.type = circle.attr("type");
        this.name = circle.attr("name");
        this.lightId = circle.attr("lightid");
        this.warpId = circle.attr("warpid");
    }

    var pm = placemark.prototype;

    pm.toJson = function() {

    };

    pm.register = function () {

    };

    pm.update = function () {

    };

    // 2つの placemark の距離を計算
    pm.distance = function (pm2) {
        return Math.ceil(0.1 * Math.sqrt(
            Math.pow(this.x - pm2.x, 2) + Math.pow(this.y - pm2.y, 2) // x^2 + y^2
        ));
    }

    // 2つの placemark を接続する edge を作成
    pm.connect = function (pm2) {
        cost = this.distance(pm2);
        return new edge(this, pm2, cost);
    };

    // この placemark を削除
    pm.delete = function (successCallback, errorCallbak) {
        url = ENDPOINT + this.id;
        asyncDelete(url, null, successCallback, errorCallbak);        
    };

    return placemark;
})();