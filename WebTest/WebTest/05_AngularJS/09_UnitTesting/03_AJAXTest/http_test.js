describe("HTTP service test", function () {

    // Arrange
    var mockScope, controller, backend;

    beforeEach(angular.mock.module("exampleApp"));

    beforeEach(angular.mock.inject(function ($httpBackend) {
        // сервис $httpBackend используется для реализации низкоуровневое API по работе с AJAX запросами 
        // для тестирования приложений, которые используют сервис $http, с помощью этого сервиса можно
        // имитировать ответы от сервера (сервис находиться в ngMock) 
        backend = $httpBackend;

        // При GET запросе по адресу productData.json вернуть объект передаваемый в метод respond
        // expect(method, url, data, headers) определяет ожидаемый запрос (параметры опциональны)    
        backend.expect("GET", "productData.json").respond(
        [{ "name": "item 1", "price": 1.20 },
        { "name": "item 2", "price": 2.42 },
        { "name": "item 3", "price": 2.02 }]);
    }));

    beforeEach(angular.mock.inject(function ($controller, $rootScope, $http) {
        mockScope = $rootScope.$new();
        $controller("defaultCtrl", {
            $scope: mockScope,
            $http: $http
        });
        // flush() / flush(count) отправляет результат или указанное количество ответов, пока этот метод не будет вызван response отправлен не будет
        backend.flush();
    }));

    // Act and Assess
    it("Ajax запрос", function () {
        // проверка что все запросы обработаны и получены ответы
        backend.verifyNoOutstandingExpectation();
    });

    it("Обработка данных", function () {
        expect(mockScope.products).toBeDefined();
        expect(mockScope.products.length).toEqual(3);
    });

    it("Последовательность данных в ответе", function () {
        expect(mockScope.products[0].name).toEqual("item 1");
        expect(mockScope.products[1].name).toEqual("item 2");
        expect(mockScope.products[2].name).toEqual("item 3");
    });
});
