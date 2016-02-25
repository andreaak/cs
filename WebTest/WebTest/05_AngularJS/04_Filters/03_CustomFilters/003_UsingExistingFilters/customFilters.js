angular.module("exampleApp")
.filter("changeCase", function () {
    return function (value, toUpper) {
        if (angular.isString(value)) {
            var processedValue = toUpper ? value.toUpperCase() : value.toLowerCase();
            return processedValue;
        } else {
            return value;
        }
    };
})
.filter("skipItems", function () {
    return function (value, count) {
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
})

/*
    Сервис $filter используется когда вам нужно в коде приложения вызывать существующие фильтры, 
    например для объединения функционала, создания оберток или для обработки данных непосредственно в функции.
    Для работы с этим сервисом его нужно просто указать в аргументах функции и AngularJS 
    используя Dependency Injection передаст $filter в качестве аргумента функции.
*/
.filter("take", function ($filter) {
    return function (data, from, count) {
        var arr = $filter("skipItems")(data, from);
        return $filter("limitTo")(arr, count);
    }
});