class PlaceMark {

    constructor($circle) {
        if (arguments.length == 1) {
            this._$circle = $circle;
            this._id = $circle.attr("id");
            this._x = $circle.attr("cx");
            this._y = $circle.attr("cy");
            this._floor = $circle.attr("floor");
            this._type = $circle.attr("type");
            this._name = $circle.attr("name");
            this._lightId = $circle.attr("lightId");
            this._warpId = $circle.attr("warpId");
        }

        // エンドポイントの設定
        if (location.hostname == "localhost") {
            this.ENDPOINT = "http://localhost:11943/api/places/";
        }
        else {
            this.ENDPOINT = "https://fujitsu-chizai.azurewebsites.net/api/places/";
        }
    }

    get className() { return "PlaceMark"; }


    serialize() {
        return {
            id: this._id,
            x: this._x,
            y: this._y,
            floor: this._floor,
            type: this._type,
            name: this._name,
            lightId: this._lightId,
            warpId: this._warpId
        };
    }

    // この placemark をサーバに登録
    register(successCallback, errorCallback) {
        var that = this;
        var callback = function (json) {
            successCallback();
            that.id = json.id; // 割り当てられたIDを反映
        };
        Http.asyncPost(this.ENDPOINT, this.serialize(), callback, errorCallback);
    }

    // この placemark をサーバに更新
    update(successCallback, errorCallback) {
        var url = this.ENDPOINT + this.id;
        Http.asyncPut(url, this.serialize(), successCallback, errorCallback);
    }

    // 2つの placemark の距離を計算
    distance(pm2) {
        return Math.ceil(0.1 * Math.sqrt(
            Math.pow(this.x - pm2.x, 2) + Math.pow(this.y - pm2.y, 2) // x^2 + y^2
        ));
    }

    // 2つの placemark を接続する edge を作成
    connect(pm2) {
        var cost = this.distance(pm2);
        return new edge(this, pm2, cost);
    }

    // この placemark を削除
    delete(successCallback, errorCallback) {
        var url = this.ENDPOINT + this.id;
        Http.asyncDelete(url, null, successCallback, errorCallback);        
    }

    dispose() {
        if (this._$circle != null) {
            this._$circle.remove();
        }
    }

    _setCircleAttr(key, value) {
        if (this._$circle != null) {
            this._$circle.attr(key, value);
            this._updateTooltip();
        }
    }

    _updateTooltip() {
        var title = "";
        this._$circle.attr("title", title);
    }

    // Properties
    get id() { return this._id; }
    set id(id) {
        this._id = id;
        this._setCircleAttr("id", this._id);
    }

    get x() { return this._x; }
    set x(x) {
        this._x = x;
        this._setCircleAttr("cx", this._x);
    }

    get y() { return this._y; }
    set y(y) {
        this._y = y;
        this._setCircleAttr("cy", this._y);
    }

    get floor() { return this._floor; }
    set floor(floor) {
        this._floor = floor;
        this._setCircleAttr("floor", this._floor);
    }

    get type() { return this._type; }
    set type(type) {
        this._type = type;
        this._setCircleAttr("type", this._type);
        this._setCircleAttr("fill", typeColor[this._type]);
    }

    get name() { return this._name; }
    set name(name) {
        this._name = name;
        this._setCircleAttr("name", this._name);
    }

    get lightId() { return this._lightId; }
    set lightId(lightId) {
        this._lightId = lightId;
        this._setCircleAttr("lightId", this._lightId);
    }

    get warpId() { return this._warpId; }
    set warpId(warpId) {
        this._warpId = warpId;
        this._setCircleAttr("warpId", this._warpId);
    }
}
