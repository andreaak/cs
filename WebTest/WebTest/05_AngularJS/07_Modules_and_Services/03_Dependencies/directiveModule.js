// Создание нового модуля с именем directiveModule.
// Для небольших проектов создание одного модуля достаточно,
// но для сложных приложений и для ситуаций когда функциональность будет использоться в разных проэктах есть смысл создавать несколько модулй.
angular.module("directiveModule", []).directive("triButton", function () {
    return {
        scope: { counter: "=counter" },
        link: function (scope, element, attrs) {
            element.on("click", function (event) {
                console.log("Button click: " + event.target.innerText);
                scope.$apply(function () {
                    scope.counter++;
                });
            });
        }
    }
});