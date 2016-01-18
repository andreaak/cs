angular.module("exampleApp", [])
.directive("orderedList", function () {
    return {
        link: function (scope, element, attributes) {

            var data = scope[attributes["orderedList"] || attributes["listSource"]]; // listSource применяется если директива используется как элемент
            var expression = attributes["property"] || "price | currency";

            if (angular.isArray(data)) {
                var elem = angular.element("<ol>");

                if (element[0].nodeName == "#comment") { // проверка необходима для того чтобы применять директиву через комментарии
                    element.parent().append(elem);
                } else {
                    element.append(elem);
                }

                for (var i = 0; i < data.length; i++) {
                    var itemElement = angular.element("<li>").text(scope.$eval(expression, data[i]));
                    elem.append(itemElement);
                }
            }
        },
        restrict: "EACM"
        // restrict определяет способ применения директивы:
        // E - как элемент
        // A - как атрибут
        // C - как класс
        // M - как комментарий
    }
})
.controller("defaultCtrl", function ($scope) {
    $scope.items = [
        { name: "Table", price: 44.10 },
        { name: "Chair", price: 21.20 },
        { name: "Pillow", price: 12.20 },
        { name: "Picture", price: 112.70 },
        { name: "Lamp", price: 28.31 }
    ];
});
