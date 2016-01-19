angular.module("exampleApp", [])
.filter("changeCase", function () {
    // value - данные, для которых применяется фильтр
    // toUpper - аргумент, передаваемый фильтру
    return function (value, toUpper) {
        // проверка переменной value на наличие строки
        if (angular.isString(value)) {
            var processedValue = toUpper ? value.toUpperCase() : value.toLowerCase();
            return processedValue;
        } else {
            return value;
        }
    };
});
