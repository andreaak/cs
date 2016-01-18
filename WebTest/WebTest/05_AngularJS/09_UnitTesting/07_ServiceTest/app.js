angular.module("exampleApp", [])
.factory("counterService", function () {
    // Сервис для инкримента и получения значения
    var counter = 0;
    return {
        incrementCounter: function () {
            counter++;
        },
        getCounter: function () {
            return counter;
        }
    }
});