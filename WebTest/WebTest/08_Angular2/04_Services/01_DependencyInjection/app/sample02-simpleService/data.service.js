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
// Декоратор указывает, что данный класс можно создавать с помощью инжектора.
// Декораторы @Cmponent, @Pipe, @Directive производные от Injectable
// Для DataService декоратор, в данном случае, не обязателен, так как сам сервис не создается с помощью инжектора.
// Как только у сервиса появятся зависимости (будет добавлен конструктор, который требует определения зависимостей)
// Angular начнет выбрасывать исключение.
// По соглашению декоратор @Injectable добавляется всем сервисам даже если не является обязательным
// 1) Нет необходимости добавлять декоратор, когда появятся зависимости
// 2) Все сервисы имеют одинаковый вид
var DataService = (function () {
    function DataService() {
    }
    DataService.prototype.getData = function () {
        var items = [];
        for (var i = 0; i < 10; i++) {
            items[i] = "Item " + i;
        }
        return items;
    };
    DataService = __decorate([
        core_1.Injectable(), 
        __metadata('design:paramtypes', [])
    ], DataService);
    return DataService;
}());
exports.DataService = DataService;
//# sourceMappingURL=data.service.js.map