describe("Filter Test", function () {

    var filterInstance;

    beforeEach(angular.mock.module("exampleApp"));

    beforeEach(angular.mock.inject(function ($filter) {
        filterInstance = $filter("changeCase");
    }));

    it("Перевод в нижний регистр", inject(function () {
        var result = filterInstance("Hello World");
        expect(result).toEqual("hello world");
    }));

    it("Перевод в верхний регистр", inject(function () {
        var result = filterInstance("Hello World", true);
        expect(result).toEqual("HELLO WORLD");
    }));
    
});