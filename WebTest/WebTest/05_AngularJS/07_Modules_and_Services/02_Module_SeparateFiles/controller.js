// контроллер defaultCtrl
angular.module("exampleApp").controller("defaultCtrl", function ($scope) {

    $scope.buttons = {
        names: ["Button #1", "Button #2", "Button #3"],
        totalClicks: 0
    };

    $scope.$watch('buttons.totalClicks', function (newVal) {
        console.log("Total click count: " + newVal);
    });
});