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
var index_1 = require("../index");
var Sample1Component = (function () {
    function Sample1Component(logger) {
        this.logger = logger;
    }
    Sample1Component.prototype.logMessage = function () {
        this.logger.log(this.message);
    };
    Sample1Component = __decorate([
        core_1.Component({
            moduleId: module.id,
            selector: "my-sample1",
            templateUrl: "sample1.component.html",
            providers: [{ provide: index_1.Logger, useClass: index_1.Logger }] // поставить комментарий
        }), 
        __metadata('design:paramtypes', [index_1.Logger])
    ], Sample1Component);
    return Sample1Component;
}());
exports.Sample1Component = Sample1Component;
//# sourceMappingURL=sample1.component.js.map