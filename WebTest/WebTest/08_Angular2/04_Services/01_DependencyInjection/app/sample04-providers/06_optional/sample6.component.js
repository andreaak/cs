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
var __param = (this && this.__param) || function (paramIndex, decorator) {
    return function (target, key) { decorator(target, key, paramIndex); }
};
var core_1 = require("@angular/core");
var index_1 = require("../index");
var Sample6Component = (function () {
    // @Optional() - зависимость является не обязательной. Если инжектор не сможет ее создать, поле logger будет null
    function Sample6Component(logger) {
        this.logger = logger;
    }
    Sample6Component.prototype.log = function () {
        if (this.logger) {
            this.logger.log("Option 1");
        }
        else {
            console.log("Option 2");
        }
    };
    Sample6Component = __decorate([
        core_1.Component({
            moduleId: module.id,
            selector: "my-sample6",
            templateUrl: "sample6.component.html",
            providers: [index_1.Logger]
        }),
        __param(0, core_1.Optional()), 
        __metadata('design:paramtypes', [index_1.Logger])
    ], Sample6Component);
    return Sample6Component;
}());
exports.Sample6Component = Sample6Component;
//# sourceMappingURL=sample6.component.js.map