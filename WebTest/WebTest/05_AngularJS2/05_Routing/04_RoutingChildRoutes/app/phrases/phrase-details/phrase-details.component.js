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
var phrase_service_1 = require("../../shared/phrase.service");
var PhraseDetailsComponent = (function () {
    // ActivatedRoute - содержит информацию о маршруте связанную с компонентом, который загружен в outlet
    function PhraseDetailsComponent(router, activatedRoute, service) {
        this.router = router;
        this.activatedRoute = activatedRoute;
        this.service = service;
    }
    PhraseDetailsComponent.prototype.ngOnInit = function () {
        // params - параметры текущего маршрута. Данное свойство является Observable объектом
        // Если параметры будут изменены - произойдет событие и компонент узнает о изменениях.
        var _this = this;
        // forEach - устанавливаем обработчик на каждое изменение params
        this.activatedRoute.params.forEach(function (params) {
            var id = +params["id"]; // конвертируем значение параметра id в тип number
            _this.service
                .getPhrase(id) // обращаемся к сервису и запрашиваем фразу по id. Получаем Promise
                .then(function (result) { return _this.phrase = result; }); // как только Promise перейдет в состояние resolved присваиваем его значение свойству phrase
        });
    };
    PhraseDetailsComponent = __decorate([
        core_1.Component({
            moduleId: module.id,
            selector: "phrase-details",
            templateUrl: "phrase-details.component.html"
        }), 
        __metadata('design:paramtypes', [router_1.Router, router_1.ActivatedRoute, phrase_service_1.PhraseService])
    ], PhraseDetailsComponent);
    return PhraseDetailsComponent;
}());
exports.PhraseDetailsComponent = PhraseDetailsComponent;
//# sourceMappingURL=phrase-details.component.js.map