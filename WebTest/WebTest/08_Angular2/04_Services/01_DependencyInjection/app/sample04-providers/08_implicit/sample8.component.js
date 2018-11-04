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
var Sample8Component = (function () {
    function Sample8Component() {
    }
    Sample8Component.prototype.onClick = function () {
        var injector = core_1.ReflectiveInjector.resolveAndCreate([index_1.Logger]);
        var logger = injector.get(index_1.Logger);
        logger.log("Hello world");
    };
    Sample8Component = __decorate([
        core_1.Component({
            moduleId: module.id,
            selector: "my-sample8",
            templateUrl: "sample8.component.html"
        }), 
        __metadata('design:paramtypes', [])
    ], Sample8Component);
    return Sample8Component;
}());
exports.Sample8Component = Sample8Component;
//# sourceMappingURL=sample8.component.js.map