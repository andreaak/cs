angular.module("exampleApp", [])
.directive("orderedList", function () {
    // link function - производит связывание директивы и HTML разметки.
    // Данная функция вызывается каждый раз когда новый экземпляр директивы создается AngularJS
    // Аргументы функции (не предоставляются через Dependecy Injection):
    // 1 - scope к которому применяется директива
    // 2 - HTML элемент, к которому применяется директива
    // 3 - атрибуты HTML элемента
    return function (scope, element, attributes) {

        var attrValue = attributes["orderedList"];  // получаем значение атрибута (ordered-list="items")
        var data = scope[attrValue];                // получаем значение из scope (scope[items])

        if (angular.isArray(data)) {
            // angular.element - метод преобразовывает строку или DOM элемент в jQuery объект
            var e = angular.element("<ol>");
            element.append(e); // добавляем ol к элементу для которого установлена директива
            for (var i = 0; i < data.length; i++) {
                // Создаем li элементы заполняя их данными из массива data
                e.append(angular.element('<li>').text(data[i].name));
            }
        }
    }
})