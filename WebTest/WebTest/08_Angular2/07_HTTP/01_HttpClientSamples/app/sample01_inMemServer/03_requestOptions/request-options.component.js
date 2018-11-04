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
var RequestOptionsComponent = (function () {
    function RequestOptionsComponent(http) {
        this.http = http;
        this.url = "app/items";
    }
    RequestOptionsComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.http.get(this.url).subscribe(function (result) { return _this.itemArray = result.json().data; }, function (error) { return console.log(error.statusText); });
    };
    RequestOptionsComponent.prototype.clickHandler = function () {
        var _this = this;
        // определение пользовательских заголовков
        var myHeaders = new http_1.Headers({
            "Content-Type": "application/json",
            "X-MyHeader": "Hello world"
        });
        // создание опций для запроса
        var options = new http_1.RequestOptions({ headers: myHeaders });
        // создание объекта для отправки на сервер
        var data = {
            id: this.id,
            name: this.name
        };
        // post запроса с указанием адреса, данных и опций
        // для того чтобы увидеть данные запроса в Developer Tools во вкладке Network 
        // закомментируйте строку импорта и настройки модуля InMemoryWebApiModule в root module (app.module.ts)
        this.http.post(this.url, data, options).subscribe(function (result) {
            var json = result.json();
            if (json)
                _this.itemArray.push(json.data);
        }, function (error) { return console.log(error); });
    };
    RequestOptionsComponent = __decorate([
        core_1.Component({
            moduleId: module.id,
            selector: "request-options",
            templateUrl: "request-options.component.html"
        }), 
        __metadata('design:paramtypes', [http_1.Http])
    ], RequestOptionsComponent);
    return RequestOptionsComponent;
}());
exports.RequestOptionsComponent = RequestOptionsComponent;
//# sourceMappingURL=request-options.component.js.map