/*
Для того чтобы создать пользовательский фильтр нужно использовать метод filter который есть у модуля: Module.filter(). 
Этот метод принимает 2 аргумента:
    1) Имя фильтра;
    2) Factory function - функция задача которой венруть "worker function". 
                            Worker function будет использоваться для фильтрации значения.
Далее его можно использовать в разметке как обычный встроенный фильтр.
При вызове метода module без второго аргумента (массива зависимотей) 
AngularJS производит поиск уже существующего модуля для которого создается фильтр.
*/
angular.module("exampleApp")
.filter("changeCase", function () {
    // value - данные для которых применяется фильтр
    // toUpper - аргумент передаваемый фильтру
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