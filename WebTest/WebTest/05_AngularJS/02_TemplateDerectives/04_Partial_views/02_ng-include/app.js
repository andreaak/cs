var app = angular.module("SampleApp", []);

app.controller("SampleAppCtrl", function ($scope) {

    $scope.items = [{ name: "Item 1", value: "Value 1" },
        { name: "Item 2", value: "Value 2" },
        { name: "Item 3", value: "Value 3" }];

    $scope.tableView = "table.html";

    $scope.listView = "list.html";

    $scope.url = $scope.tableView;

    $scope.showList = function () {
        $scope.url = $scope.listView;
    }

    $scope.showTable = function () {
        $scope.url = $scope.tableView;
    }
});