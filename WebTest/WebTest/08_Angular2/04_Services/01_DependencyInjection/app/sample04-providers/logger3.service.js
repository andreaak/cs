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
var Logger3 = (function () {
    function Logger3(prefix) {
        this.prefix = prefix;
    }
    Logger3.prototype.log = function (message) {
        console.log(this.prefix + "_" + message);
    };
    Logger3 = __decorate([
        core_1.Injectable(), 
        __metadata('design:paramtypes', [String])
    ], Logger3);
    return Logger3;
}());
exports.Logger3 = Logger3;
//# sourceMappingURL=logger3.service.js.map