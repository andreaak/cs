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
// Примеры из директории sample01_inMemServer используют angular2-in-memory-web-api
// для того чтобы тестировать приложение обрабатывая http запросы без реального сервера
// Настройка angular2-in-memory-web-api происходит в app.module
// Логика серверной стороны находиться в файле inMemoryServer.ts
var GetRequestComponent = (function () {
    function GetRequestComponent(http) {
        this.http = http;
    }
    GetRequestComponent.prototype.clickHandler = function () {
        var _this = this;
        // this.http.get - отправка get запроса по указанному адресу
        // метод возвращает объект Observable из библиотеки RxJS
        // с помощью метода subscribe подписываемся на событие
        // событие произойдет после получение ответа от сервера.
        this.http.get("app/items").subscribe(function (result) { return _this.itemArray = result.json().data; }, function (error) { return console.log(error.statusText); });
    };
    GetRequestComponent = __decorate([
        core_1.Component({
            moduleId: module.id,
            selector: "get-request",
            templateUrl: "get-request.component.html"
        }), 
        __metadata('design:paramtypes', [http_1.Http])
    ], GetRequestComponent);
    return GetRequestComponent;
}());
exports.GetRequestComponent = GetRequestComponent;
//# sourceMappingURL=get-request.component.js.map