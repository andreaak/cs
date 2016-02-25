var app = angular.module("SampleApp", []);

app.controller("SampleAppCtrl", function ($scope) {

    $scope.items = [{ name: "Item 1", value: "Value 1" },
        { name: "Item 2", value: "Value 2" },
        { name: "Item 3", value: "Value 3" }];

    $scope.url = "table.html";
});