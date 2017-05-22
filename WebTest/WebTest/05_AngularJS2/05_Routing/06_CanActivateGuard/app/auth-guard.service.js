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
/*
    Guard - механизм для выполнения проверок перед активацией и деактивацией маршрута

    CanActivate - Определяет возможность активации маршрута
    CanActivateChild - Определяет возможность активации дочерних маршрутов текущего маршрута
    CanDeactivate - Определяет можно ли уйти с текущего маршрута
    CanLoad - Определяет может ли модуль загрузиться с использованием lazy loading

    Установка объектов Guard происходит при настройке маршрутизации.
    В данном примере Guard используется в файле /admin/admin-routing.module.ts
    Также, для AuthGuard необходимо установить провайдер (в данном примере провайдер установлен в app.module.ts)
*/
var AuthGuard = (function () {
    function AuthGuard() {
    }
    // Observable<boolean>|Promise<boolean>|boolean - возможные результаты работы метода
    // Если возвращенное значение true маршрут будет активирован, иначе - нет
    AuthGuard.prototype.canActivate = function () {
        var value = true;
        console.log("AuthGuard canActivate return " + value);
        return value;
    };
    AuthGuard = __decorate([
        core_1.Injectable(), 
        __metadata('design:paramtypes', [])
    ], AuthGuard);
    return AuthGuard;
}());
exports.AuthGuard = AuthGuard;
//# sourceMappingURL=auth-guard.service.js.map