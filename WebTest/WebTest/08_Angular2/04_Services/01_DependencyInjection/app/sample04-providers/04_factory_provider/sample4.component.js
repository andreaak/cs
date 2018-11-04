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
var Sample4Component = (function () {
    function Sample4Component(logger) {
        this.logger = logger;
    }
    Sample4Component.prototype.logMessage = function () {
        this.logger.log(this.message);
    };
    Sample4Component = __decorate([
        core_1.Component({
            moduleId: module.id,
            selector: "my-sample4",
            templateUrl: "sample4.component.html",
            providers: [{
                    provide: index_1.Logger,
                    useFactory: function () {
                        return new index_1.Logger3("test");
                    }
                }]
        }), 
        __metadata('design:paramtypes', [index_1.Logger])
    ], Sample4Component);
    return Sample4Component;
}());
exports.Sample4Component = Sample4Component;
//# sourceMappingURL=sample4.component.js.map