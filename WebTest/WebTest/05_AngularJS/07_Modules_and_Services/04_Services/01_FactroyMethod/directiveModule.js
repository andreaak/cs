// данный модуль использует модуль customServices, в котором определен сервис для логирования
angular.module("directiveModule", ["customServices"])
// AngularJS при вызове фабричной функции анализирует названия ее аргументов и производит 
// внедрение зависимостей - передает экземпляр сервиса для логирования в параметр с именем logService
.directive("triButton", function (logService) {
    return {
        scope: { counter: "=counter" },
        link: function (scope, element, attrs) {
            element.on("click", function (event) {
                logService.log("Button click: " + event.target.innerText);
                scope.$apply(function () {
                    scope.counter++;
                });
            });
        }
    }
});