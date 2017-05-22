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
var PhraseListComponent = (function () {
    function PhraseListComponent(router, activatedRoute, phraseService) {
        this.router = router;
        this.activatedRoute = activatedRoute;
        this.phraseService = phraseService;
    }
    PhraseListComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.activatedRoute.params.forEach(function (params) {
            _this.selectedId = +params["id"]; // чтение опционального параметра
            _this.phraseService
                .getAll()
                .then(function (result) { return _this.phrases = result; });
        });
    };
    PhraseListComponent.prototype.isSelected = function (phrase) {
        return phrase.id == this.selectedId;
    };
    PhraseListComponent.prototype.onSelect = function (selected) {
        // При клике по элементу списка перенаправляем пользователя по адресу /phrases/id
        // адрес с обязательным параметром указан в настройках маршрутизации в файле app.routes.ts 
        this.router.navigate(["/phrases", selected.id]);
    };
    PhraseListComponent = __decorate([
        core_1.Component({
            moduleId: module.id,
            selector: "phrase-list",
            templateUrl: "phrase-list.component.html",
            styleUrls: ["phrase-list.component.css"]
        }), 
        __metadata('design:paramtypes', [router_1.Router, router_1.ActivatedRoute, phrase_service_1.PhraseService])
    ], PhraseListComponent);
    return PhraseListComponent;
}());
exports.PhraseListComponent = PhraseListComponent;
//# sourceMappingURL=phrase-list.component.js.map