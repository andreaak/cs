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
var router_1 = require("@angular/router");
var PhraseDetailsComponent = (function () {
    function PhraseDetailsComponent(router, activatedRoute) {
        this.router = router;
        this.activatedRoute = activatedRoute;
    }
    PhraseDetailsComponent.prototype.ngOnInit = function () {
        var _this = this;
        // phrase - инициализируется с помощью PhraseDetailsResolve объекта, который указан
        // в настройках системы маршрутизации.
        // на activatedRoute обращаемся к свойству data, а не param как в прошлых примерах.
        // Данный компонент избавился от зависимости PhraseService
        this.activatedRoute.data.forEach(function (data) {
            _this.phrase = data.phrase;
            _this.editValue = data.phrase.value;
            _this.editLanguage = data.phrase.language;
        });
    };
    // метод для сохранения изменений сделанных пользователем.
    PhraseDetailsComponent.prototype.save = function () {
        this.phrase.value = this.editValue;
        this.phrase.language = this.editLanguage;
        this.goToPhrasesList();
    };
    PhraseDetailsComponent.prototype.goToPhrasesList = function () {
        var pId = this.phrase ? this.phrase.id : null;
        this.router.navigate(["../", { id: pId }], { relativeTo: this.activatedRoute });
    };
    // метод для проверки возможности перенаправления пользователя на другой маршрут
    // если метод возвращает true перенаправление возможно
    // если метод вернет false пользователь получит уведомление с просьбой подтвердить переход
    // Данный метод будет использоваться при работе с CanDeactivateGuard (shared/can-deactivate-guard.service.ts)
    PhraseDetailsComponent.prototype.canDeactivate = function () {
        if (!this.phrase) {
            return true;
        }
        if (this.phrase.value == this.editValue && this.phrase.language == this.editLanguage) {
            return true;
        }
        return confirm("Вы не сохранили изменения. Уйти со страницы?");
    };
    PhraseDetailsComponent = __decorate([
        core_1.Component({
            moduleId: module.id,
            selector: "phrase-details",
            templateUrl: "phrase-details.component.html"
        }), 
        __metadata('design:paramtypes', [router_1.Router, router_1.ActivatedRoute])
    ], PhraseDetailsComponent);
    return PhraseDetailsComponent;
}());
exports.PhraseDetailsComponent = PhraseDetailsComponent;
//# sourceMappingURL=phrase-details.component.js.map