var edge = (function () {
    var ENDPOINT = "https://fujitsu-chizai.azurewebsites.net/api/edges/";
    if (location.hostname == "localhost") {
        ENDPOINT = "http://localhost:11943/api/edges/";
    }

    // constructor
    var edge = function (arg1, arg2, arg3) {
        // overload 1
        var edge1 = function (instance, path) {
            instance.$dom = $(path);
            instance.id1 = path.attr("id1");
            instance.id2 = path.attr("id2");
            instance.cost = path.attr("cost");
        };
        // overload 2
        var edge2 = function edge2(instance, pm1, pm2, cost) {
            // path 要素作成
            var line = d3.svg.line()
                .x(function (d) { return d[0]; })
                .y(function (d) { return d[1]; });
            var path = document.createElementNS("http://www.w3.org/2000/svg", "path");
            path.setAttribute("d", line([[pm1.x, pm1.y], [pm2.x, pm2.y]]));
            path.setAttribute('stroke', 'gray');
            path.setAttribute('stroke-width', 5);
            path.setAttribute('id1', pm1.id);
            path.setAttribute('id2', pm2.id);
            path.setAttribute('cost', cost);

            // 円に重ならないように調整
            var len = path.getTotalLength() - 20 * 2;
            path.setAttribute('stroke-dasharray', '0 20 ' + len + ' 20');
            path.setAttribute('stroke-dashoffset', 0)

            instance.$dom = $(path);
            instance.id1 = pm1.id;
            instance.id2 = pm2.id;
            instance.cost = cost;
        };
        
        switch (arguments.length) {
            case 1: edge1(this, arg1); break;
            case 3: edge2(this, arg1, arg2, arg3); break;
        }
    }

    var e = edge.prototype;
    e.className = "Edge";

    // この edge を登録
    e.register = function (successCallback, errorCallbak) {
        data = {
            placeMarkId1: this.id1,
            placeMarkId2: this.id2,
            cost: this.cost
        };
        Http.asyncPost(ENDPOINT, data, successCallback, errorCallbak);
    };

    // この edge を削除
    e.delete = function (successCallback, errorCallbak) {
        url = ENDPOINT + "?id1=" + this.id1 + "&id2=" + this.id2;
        Http.asyncDelete(url, null, successCallback, errorCallbak);
    };

    e.dispose = function () {
        this.$dom.remove();
    };

    return edge;
})();