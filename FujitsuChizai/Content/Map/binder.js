class Binder {
    constructor() { }


    // placemark の値を form から更新
    updatePlaecmark() {
        this._onFormChanged();
    }

    _onFormChanged() {
        this._bindForm(this._form);
    }

    _bindForm(form) {
        if (this._placemark == null) return;
        this._placemark.id = form.id;
        this._placemark.x = form.x;
        this._placemark.y = form.y;
        this._placemark.floor = form.floor;
        this._placemark.type = form.type;
        this._placemark.name = form.name;
        this._placemark.lightId = form.lightId;
        this._placemark.warpId = form.warpId;
    }

    // form の値を placemark から更新
    updateForm() {
        this._onPlaceMarkChanged();
    }    

    _onPlaceMarkChanged() {
        this._bindPlaceMark(this._placemark);
    }

    _bindPlaceMark(pm) {
        if (this._form == null) return;
        this._form.id = pm.id;
        this._form.x = pm.x;
        this._form.y = pm.y;
        this._form.floor = pm.floor;
        this._form.type = pm.type;
        this._form.name = pm.name;
        this._form.lightId = pm.lightId;
        this._form.warpId = pm.warpId;

        this._form.updateFormDisplay(this._form.$form);
    }


    // Properties
    get form() { return this._form; }
    set form(form) {
        // unsubscribe
        if (this._form != null) {
            this._form.onChanged = null;
            this._form.submit = null;
        }

        // subscribe
        this._form = form;
        var that = this;
        this._form.onChanged = function () {
            that._onFormChanged();
        };
        this._form.submit = function (mode, successCallback, errorCallback, confirmDialog) {
            confirmDialog(function () {
                switch (mode) {
                    case ControlMode.REGISTER:
                        that.placemark.register(successCallback, errorCallback);
                        break;
                    case ControlMode.UPDATE:
                        that.placemark.update(successCallback, errorCallback);
                        break;
                }
            }, function () { });
        };
    }

    get placemark() { return this._placemark; }
    set placemark(placemark) {
        if (this._placemark != null) {
            this._placemark.onChanged = null;
        }

        this._placemark = placemark;
        var that = this;
        this._placemark.onChanged = function () {
            that._onPlaceMarkChanged();
        };
    }
}
