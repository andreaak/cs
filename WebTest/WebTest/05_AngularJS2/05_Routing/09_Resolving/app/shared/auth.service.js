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
var Observable_1 = require("rxjs/Observable");
// подключение необходимых методов для работы с Observable
require("rxjs/add/observable/of");
require("rxjs/add/operator/do");
require("rxjs/add/operator/delay");
var AuthService = (function () {
    function AuthService() {
        this.isLoggedIn = false;
    }
    AuthService.prototype.login = function (login, password) {
        var _this = this;
        return Observable_1.Observable // создание Observable объекта
            .of(true) // добавление элементов в последовательность
            .delay(1000) // задержка на 1 сек.
            .do(function (val) {
            if (login == "admin" && password == "qwerty")
                _this.isLoggedIn = true;
            return _this.isLoggedIn;
        });
    };
    AuthService.prototype.logout = function () {
        this.isLoggedIn = false;
    };
    AuthService = __decorate([
        core_1.Injectable(), 
        __metadata('design:paramtypes', [])
    ], AuthService);
    return AuthService;
}());
exports.AuthService = AuthService;
//# sourceMappingURL=auth.service.js.map