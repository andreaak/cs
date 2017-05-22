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
var wiki_service_1 = require("./wiki.service");
var WikiComponent = (function () {
    function WikiComponent(wiki) {
        this.wiki = wiki;
        this.items = [];
    }
    WikiComponent.prototype.search = function (term) {
        var _this = this;
        this.wiki.search(term).subscribe(function (response) { return _this.items = response; });
    };
    WikiComponent = __decorate([
        core_1.Component({
            moduleId: module.id,
            selector: "my-wiki",
            templateUrl: "wiki.component.html"
        }), 
        __metadata('design:paramtypes', [wiki_service_1.WikiService])
    ], WikiComponent);
    return WikiComponent;
}());
exports.WikiComponent = WikiComponent;
//# sourceMappingURL=wiki.component.js.map