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
var phrase_service_1 = require("../shared/phrase.service");
// Resolve - интерфейс указывает на то, что объект может быть поставщиком данных (data provider)
// Подобные объекты позволяют избедать проблемы связанной с тем что компонент уже отобразился пользователю а данные для компонента еще не доступны.
var PhraseDetailsResolve = (function () {
    function PhraseDetailsResolve(service, router) {
        this.service = service;
        this.router = router;
    }
    PhraseDetailsResolve.prototype.resolve = function (route) {
        var _this = this;
        var id = +route.params["id"];
        return this.service.getPhrase(id).then(function (phrase) {
            if (phrase) {
                return phrase;
            }
            else {
                _this.router.navigate(["/phrases"]);
                return false;
            }
        });
    };
    PhraseDetailsResolve = __decorate([
        core_1.Injectable(), 
        __metadata('design:paramtypes', [phrase_service_1.PhraseService, router_1.Router])
    ], PhraseDetailsResolve);
    return PhraseDetailsResolve;
}());
exports.PhraseDetailsResolve = PhraseDetailsResolve;
//# sourceMappingURL=phrase-details-resolve.service.js.map