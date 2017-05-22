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
var block2_component_1 = require("./block2.component");
var Block2HostComponent = (function () {
    function Block2HostComponent() {
    }
    Block2HostComponent.prototype.showAll = function () {
        this.blocks.forEach(function (x) { return x.show(); });
    };
    __decorate([
        core_1.ViewChildren(block2_component_1.Block2Component), 
        __metadata('design:type', core_1.QueryList)
    ], Block2HostComponent.prototype, "blocks", void 0);
    Block2HostComponent = __decorate([
        core_1.Component({
            moduleId: module.id,
            selector: "block2-host",
            templateUrl: "block2-host.component.html"
        }), 
        __metadata('design:paramtypes', [])
    ], Block2HostComponent);
    return Block2HostComponent;
}());
exports.Block2HostComponent = Block2HostComponent;
//# sourceMappingURL=block2-host.component.js.map