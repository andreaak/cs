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
var common_1 = require("@angular/common");
var index_1 = require("./index");
var LifecycleSampleModule = (function () {
    function LifecycleSampleModule() {
    }
    LifecycleSampleModule = __decorate([
        core_1.NgModule({
            imports: [common_1.CommonModule],
            declarations: [
                index_1.Sample1Component,
                index_1.Sample1HostComponent,
                index_1.Sample2Component,
                index_1.Sample2HostComponent,
                index_1.Sample3Component,
                index_1.Sample3HostComponent,
                index_1.Sample4Component,
                index_1.Sample4HostComponent
            ]
        }), 
        __metadata('design:paramtypes', [])
    ], LifecycleSampleModule);
    return LifecycleSampleModule;
}());
exports.LifecycleSampleModule = LifecycleSampleModule;
//# sourceMappingURL=lifecycle-samples.module.js.map