describe("Directive Test", function () {

    var mockScope, compileService;

    beforeEach(angular.mock.module("exampleApp"));

    beforeEach(angular.mock.inject(function ($rootScope, $compile) { 
        mockScope = $rootScope.$new();
        compileService = $compile;
        mockScope.data = [{ name: "item 1", ptice: 1 },
            { name: "item 2", ptice: 2 },
            { name: "item 3", ptice: 3 }];
    }))

    it("Создание упорядоченного списка через директиву", function () {
        var compileFn = compileService("<div ordered-list='data'></div>");
        var elem = compileFn(mockScope);

        expect(elem.find("ol").length).toEqual(1);
        expect(elem.find("li").length).toEqual(3);
        expect(elem.find("li").eq(0).text()).toEqual("item 1");
        expect(elem.find("li").eq(1).text()).toEqual("item 2");
        expect(elem.find("li").eq(2).text()).toEqual("item 3");
    });

});