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
var NameCardComponent = (function () {
    function NameCardComponent() {
    }
    Object.defineProperty(NameCardComponent.prototype, "lastName", {
        // getter для получения значения закрытого поля _lastName
        get: function () {
            return this._lastName;
        },
        // setter для установки значения закрытому полю _lastName
        set: function (value) {
            this._lastName = value;
        },
        enumerable: true,
        configurable: true
    });
    Object.defineProperty(NameCardComponent.prototype, "fullName", {
        get: function () {
            return this.firstName + " " + this.lastName;
        },
        enumerable: true,
        configurable: true
    });
    NameCardComponent = __decorate([
        core_1.Component({
            moduleId: module.id,
            selector: "namecard",
            templateUrl: "name-card.component.html",
            inputs: ["firstName", "lastName"]
        }), 
        __metadata('design:paramtypes', [])
    ], NameCardComponent);
    return NameCardComponent;
}());
exports.NameCardComponent = NameCardComponent;
//# sourceMappingURL=name-card.component.js.map