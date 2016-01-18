angular.module("exampleApp", [])
.controller("defaultCtrl", function ($scope, $interval, $timeout) {
    
    $scope.intervalCounter = 0;
    $scope.timerCounter = 0;

    // Запуск функции через интервал
    $interval(function () {
        $scope.intervalCounter++;
    }, 5000, 10);

    //// Запуск функции с задержкой
    $timeout(function () {
        $scope.timerCounter++;
    }, 5000);
    
})