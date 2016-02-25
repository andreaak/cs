angular.module("exampleApp")
.filter("skipItems", function () {

    return function (value, count) {
        // isArray - проверка, что переменная является массивом
        // isNumber - проверка, что переменная является числом
        if (angular.isArray(value) && angular.isNumber(count)) {
            if (count > value.length || count < 1) {
                return value;
            } else {
                return value.slice(count);
            }
        } else {
            return value;
        }
    }

});