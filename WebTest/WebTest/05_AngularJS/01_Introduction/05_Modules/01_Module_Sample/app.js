var customModule = angular.module('customModule', []);

// 'customModule'  Определяет модуль customModule. Можно использовать модули для настройки существующих сервисов,
//а так же определять новые сервисы, директивы, фильтры и т.д.
// [] Модули могут зависить от других модулей.

customModule.controller('petCtrl', function ($scope) {

    $scope.pets = [
        { name: "Lucky", animal: "Dog" },
        { name: "Lucy", animal: "Cat" }
    ];
});