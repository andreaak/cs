// Для работы данного примера используется модуль ngRoute, который предоставляет интерфейс для маршрутизации и создания сложных приложений с несколькими представлениями
angular.module("exampleApp", ["ngResource", "ngRoute"])
.constant("baseUrl", "http://localhost:2403/items/")
.config(function ($locationProvider, $routeProvider) {

    $locationProvider.html5Mode(true);

    $routeProvider.when("/table", {
        templateUrl: "/Samples/03_Views/01_UrlRouting/views/table.html"
    });

    $routeProvider.when("/edit", {
        templateUrl: "/Samples/03_Views/01_UrlRouting/views/edit.html"
    });

    $routeProvider.when("/create", {
        templateUrl: "/Samples/03_Views/01_UrlRouting/views/edit.html"
    });

    $routeProvider.otherwise({
        templateUrl: "/Samples/03_Views/01_UrlRouting/views/table.html"
    });
})
.controller("defaultCtrl", function ($scope, $http, $resource, $location, baseUrl) {

    // текущее педставление
    $scope.currentView = "table";

    $scope.itemsResource = $resource(baseUrl + ":id", { id: "@id" });

    // получение всех данных из модели
    $scope.refresh = function () {
        $scope.items = $scope.itemsResource.query();

    }

    // создание нового элемента
    $scope.create = function (item) {
        new $scope.itemsResource(item).$save().then(function (newItem) {
            $scope.items.push(newItem);
            $location.path("/table");
        });
    }

    // обновление элемента
    $scope.update = function (item) {
        item.$save();
        $location.path("/table");
    }

    // удаление элемента из модели
    $scope.delete = function (item) {
        item.$delete().then(function () {
            $scope.items.splice($scope.items.indexOf(item), 1);
        });
        $location.path("/table");
    }

    // редеактирование существующего или создание нового элемента
    $scope.editOrCreate = function (item) {
        $scope.currentItem = item ? item : {};
        $location.path("/edit");
    }

    // сохранение изменений
    $scope.saveEdit = function (item) {
        if (angular.isDefined(item.id)) {
            $scope.update(item);
        } else {
            $scope.create(item);
        }
    }

    // отмена изменений и возврат в представление table
    $scope.cancelEdit = function () {
        if ($scope.currentItem && $scope.currentItem.$get) {
            $scope.currentItem.$get();
        }
        $scope.currentItem = {};
        $location.path("/table");
    }

    $scope.refresh();
});