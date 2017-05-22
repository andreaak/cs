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
var Sample3Component = (function () {
    function Sample3Component() {
    }
    // { [propertyName: string]: SimpleChange } - массив с строковыми ключами и значением типа SimpleChange
    Sample3Component.prototype.ngOnChanges = function (changes) {
        var msgArray = [];
        for (var propName in changes) {
            msgArray.push("property = " + propName + ", current value = " + changes[propName].currentValue + ", previous value = " + changes[propName].previousValue);
        }
        console.log(msgArray.join("; "));
    };
    Sample3Component = __decorate([
        core_1.Component({
            moduleId: module.id,
            selector: "sample3",
            templateUrl: "sample3.component.html",
            inputs: ["name", "value"]
        }), 
        __metadata('design:paramtypes', [])
    ], Sample3Component);
    return Sample3Component;
}());
exports.Sample3Component = Sample3Component;
//# sourceMappingURL=sample3.component.js.map