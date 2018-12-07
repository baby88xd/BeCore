var Base = {
    WhereLambda: function (Ele, otherData) {
        var Arr = new Array();
        $(Ele).find('input,textarea').each(function () {
            var _Model = {};
            _Model.Key = $(this).attr('name');
            _Model.Value = $(this).val();
            _Model.OperatorMethod = $(this).attr('date-operator');
            if (_Model.Key != '')
                Arr.push(_Model);
        });
        if (otherData != undefined && otherData != null) {
            if (Array.isArray(otherData)) {
                Arr.addRange(otherData);
            } else {
                Arr.push(otherData);
            }
        }

        //Arr.forEach
        Conditions = JSON.stringify(Arr)
    }
}





//扩展的Array方法
Array.prototype.addRange = function (other_array) {
    /* you should include a test to check whether other_array really is an array */
    other_array.forEach(function (v) { this.push(v) }, this);
}