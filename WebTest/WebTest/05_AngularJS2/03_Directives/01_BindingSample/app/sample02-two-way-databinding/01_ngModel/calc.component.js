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
var CalcComponent = (function () {
    function CalcComponent() {
        this.xValue = 0;
        this.yValue = 0;
    }
    CalcComponent.prototype.calculate = function () {
        this.result = +this.xValue + +this.yValue;
    };
    CalcComponent.prototype.reset = function () {
        this.xValue = this.yValue = 0;
        this.result = undefined;
    };
    CalcComponent = __decorate([
        core_1.Component({
            moduleId: module.id,
            selector: "my-calc",
            templateUrl: "calc.component.html"
        }), 
        __metadata('design:paramtypes', [])
    ], CalcComponent);
    return CalcComponent;
}());
exports.CalcComponent = CalcComponent;
//# sourceMappingURL=calc.component.js.map