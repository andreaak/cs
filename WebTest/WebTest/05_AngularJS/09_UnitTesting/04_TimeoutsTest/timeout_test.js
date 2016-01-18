describe("Interval and Timeout test", function () {

    // Arrange
    var mockScope, mockInterval, mockTimeout, controller;

    beforeEach(angular.mock.module("exampleApp"));

    beforeEach(angular.mock.inject(function ($controller, $rootScope, $interval, $timeout) {
        //сервисы $interval и $timeout предоставляют возможность для тестировани callback функций
        mockScope = $rootScope.$new();
        mockInterval = $interval;
        mockTimeout = $timeout;

        controller = $controller("defaultCtrl", {
            $scope: mockScope,
            $interval: mockInterval,
            $timeout: mockTimeout
        });
    }));

    it("Ограничиваем интервал до 10 обновлений", function () {
        for (var i = 0; i < 11; i++) {
            // устанавливаем таймер на 5000 миллисикунд
            mockInterval.flush(5000);
        }
        expect(mockScope.intervalCounter).toEqual(10);
    });

    it("Изменение таймера", function () {
        mockTimeout.flush(5000);
        expect(mockScope.timerCounter).toEqual(1);
    });
   
});