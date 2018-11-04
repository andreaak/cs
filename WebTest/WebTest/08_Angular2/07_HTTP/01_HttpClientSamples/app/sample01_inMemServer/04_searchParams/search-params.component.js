"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
var core_1 = require("@angular/core");
var http_1 = require("@angular/http");
var SearchParamsComponent = (function () {
    function SearchParamsComponent(http) {
        this.http = http;
        this.url = "app/items";
    }
    SearchParamsComponent.prototype.clickHandler = function () {
        var params = new http_1.URLSearchParams();
        params.set("a", "1");
        params.set("b", "value");
        // метод get с параметром search будет выполнять запрос по следующему адресу
        // http://localhost:3000/app/items?a=1&b=value
        // Для того чтобы увидеть запрос в браузере закомментируйте настройку in memory web api в app.module.ts 
        this.http.get(this.url, { search: params })
            .subscribe(function (response) { return console.log("success"); }, function (error) { return console.log("error"); });
    };
    SearchParamsComponent = __decorate([
        core_1.Component({
            moduleId: module.id,
            selector: "search-params",
            templateUrl: "search-params.component.html"
        }), 
        __metadata('design:paramtypes', [http_1.Http])
    ], SearchParamsComponent);
    return SearchParamsComponent;
}());
exports.SearchParamsComponent = SearchParamsComponent;
//# sourceMappingURL=search-params.component.js.map