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
var forms_1 = require("@angular/forms");
var common_1 = require("@angular/common");
var phrase_list_component_1 = require("./phrase-list/phrase-list.component");
var phrase_details_component_1 = require("./phrase-details/phrase-details.component");
var phrases_routing_module_1 = require("./phrases-routing.module");
var PhrasesModule = (function () {
    function PhrasesModule() {
    }
    PhrasesModule = __decorate([
        core_1.NgModule({
            imports: [
                common_1.CommonModule,
                forms_1.FormsModule,
                phrases_routing_module_1.PhrasesRoutingModule // настройки маршрутизации для модуля PhrasesModule
            ],
            declarations: [
                phrase_details_component_1.PhraseDetailsComponent,
                phrase_list_component_1.PhraseListComponent
            ]
        }), 
        __metadata('design:paramtypes', [])
    ], PhrasesModule);
    return PhrasesModule;
}());
exports.PhrasesModule = PhrasesModule;
//# sourceMappingURL=phrases.module.js.map