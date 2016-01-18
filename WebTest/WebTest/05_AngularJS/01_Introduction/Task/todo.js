var todoModel = (function () {

    var _data = [];

    function _addItem(name, duedate, description, completed) {
        _data.push({
            id: getCurrentId(),
            name: name,
            duedate: duedate,
            description: description,
            completed: completed
        });
    }

    function _removeItem(id) {
        _data.forEach(function (e, index) {
            if (e.id == id) {
                _data.splice(index, 1);
            }
        })
    }

    function _updateItem(id, value) {
        _data.forEach(function (e, index) {
            if (e.id == id) {
                _data[index] = value;
            }
        })
    }

    function _save() {
        // второй параметр - функция, которая удаляет специальное свойство добавляемое angularJS для отслеживания дубликатов 
        // http://mutablethought.com/2013/04/25/angular-js-ng-repeat-no-longer-allowing-duplicates/
        window.localStorage["tasks"] = JSON.stringify(_data, function (key, val) {
            if (key == '$$hashKey') {
                return undefined;
            }
            return val
        });
    }

    function _read() {
        var temp = window.localStorage["tasks"]

        if (!temp) _data = [];
        else _data = JSON.parse(temp);

        return _data;
    }

    function getCurrentId() {
        if (!_data || _data.length == 0) return 0;
        else return _data[_data.length - 1].id++;
    }

    return {
        data: _data,
        addItem: _addItem,
        updateItem: _updateItem,
        removeItem: _removeItem,
        save: _save,
        read: _read
    };

})();