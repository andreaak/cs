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
var Sample3HostComponent = (function () {
    function Sample3HostComponent() {
        this.counter = 0;
    }
    Sample3HostComponent.prototype.changeName = function () {
        this.nameValue = "value " + new Date().getTime();
    };
    Sample3HostComponent.prototype.changeValue = function () {
        this.counter++;
    };
    Sample3HostComponent.prototype.changeBoth = function () {
        this.nameValue = "value " + new Date().getTime();
        this.counter++;
    };
    Sample3HostComponent = __decorate([
        core_1.Component({
            moduleId: module.id,
            selector: "sample3-host",
            templateUrl: "sample3-host.component.html"
        }), 
        __metadata('design:paramtypes', [])
    ], Sample3HostComponent);
    return Sample3HostComponent;
}());
exports.Sample3HostComponent = Sample3HostComponent;
//# sourceMappingURL=sample3-host.component.js.map