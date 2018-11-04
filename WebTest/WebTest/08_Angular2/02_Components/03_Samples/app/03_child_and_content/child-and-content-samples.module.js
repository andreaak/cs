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
var ChildAndContentSampleModule = (function () {
    function ChildAndContentSampleModule() {
    }
    ChildAndContentSampleModule = __decorate([
        core_1.NgModule({
            imports: [common_1.CommonModule],
            declarations: [
                index_1.BlockComponent,
                index_1.BlockHostComponent,
                index_1.Block2Component,
                index_1.Block2HostComponent,
                index_1.ItemComponent,
                index_1.ListComponent,
                index_1.ListHostComponent
            ]
        }), 
        __metadata('design:paramtypes', [])
    ], ChildAndContentSampleModule);
    return ChildAndContentSampleModule;
}());
exports.ChildAndContentSampleModule = ChildAndContentSampleModule;
//# sourceMappingURL=child-and-content-samples.module.js.map