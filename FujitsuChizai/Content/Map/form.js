class Form {

    constructor($form, mode) {
        this._$form = $form;
        this._mode = mode;

        var that = this;

        // 選択肢更新時には表示を更新
        this._$form.find("select").change(function () {
            that.updateFormDisplay(that._$form);
        });

        // フォーム内全ての変更イベント
        this._$form.change(function () {
            that._sync();
            that.onChanged();
        });

        // フォームのsubmitを乗っ取る
        this._$form.submit(function (event) {
            event.preventDefault();
            that.submit(that._mode, that.successCallback, that.errorCallback, that.confirmDialog);
        });
    }


    // フォームの値を自身に設定
    _sync() {
        this._id = this._getFormInput("id");
        this._x = this._getFormInput("x");
        this._y = this._getFormInput("y");
        this._floor = this._getFormInput("floor");
        this._type = this._getFormInput("type");
        this._name = this._getFormInput("name");
        this._lightId = this._getFormInput("lightId");
        this._warpId = this._getFormInput("warpId");
    }

    _getFormInput(key) {
        if (this._$form != null) {
            return this._$form.find('[name="' + key + '"]').val();
        }
    }

    _setFormInput(key, value) {
        if (this._$form != null) {
            this._$form.find('[name="' + key + '"]').val(value);
            //.change()するとeventがループしてplacemarkもformも共倒れ('A`)
        }
    }


    // Properties
    get id() { return this._id; }
    set id(id) {
        this._id = id;
        this._setFormInput("id", this._id);
    }

    get x() { return this._x; }
    set x(x) {
        this._x = x;
        this._setFormInput("x", this._x);
    }

    get y() { return this._y; }
    set y(y) {
        this._y = y;
        this._setFormInput("y", this._y);
    }

    get floor() { return this._floor; }
    set floor(floor) {
        this._floor = floor;
        this._setFormInput("floor", this._floor);
    }

    get type() { return this._type; }
    set type(type) {
        this._type = type;
        this._setFormInput("type", this._type);
    }

    get name() { return this._name; }
    set name(name) {
        this._name = name;
        this._setFormInput("name", this._name);
    }

    get lightId() { return this._lightId; }
    set lightId(lightId) {
        this._lightId = lightId;
        this._setFormInput("lightId", this._lightId);
    }

    get warpId() { return this._warpId; }
    set warpId(warpId) {
        this._warpId = warpId;
        this._setFormInput("warpId", this._warpId);
    }

    get onChanged() { return this._onChanged; }
    set onChanged(onChanged) { this._onChanged = onChanged; }

    get updateFormDisplay() { return this._updateFormDisplay; }
    set updateFormDisplay(updateFormDisplay) { this._updateFormDisplay = updateFormDisplay; }

    get submit() { return this._submit; }
    set submit(submit) { this._submit = submit; }

    get confirmDialog() { return this._confirmDialog; }
    set confirmDialog(confirmDialog) { this._confirmDialog = confirmDialog; }

    get successCallback() { return this._successCallback; }
    set successCallback(successCallback) { this._successCallback = successCallback; }

    get errorCallback() { return this._errorCallback; }
    set errorCallback(errorCallback) { this._errorCallback = errorCallback; }

    get $form() { return this._$form; }

    get mode() { return this._mode; }
}
