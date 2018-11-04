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
var Counter1Component = (function () {
    function Counter1Component(counterService) {
        this.counterService = counterService;
        this.counter = 0;
    }
    Counter1Component.prototype.plusOne = function () {
        this.counterService.increment();
        this.counter = this.counterService.getValue();
    };
    Counter1Component.prototype.minusOne = function () {
        this.counterService.decrement();
        this.counter = this.counterService.getValue();
    };
    Counter1Component = __decorate([
        core_1.Component({
            moduleId: module.id,
            selector: "counter-1",
            templateUrl: "counter1.component.html",
            providers: [index_1.CounterService] // данный компонент использует свой экземпляр сервиса
        }), 
        __metadata('design:paramtypes', [index_1.CounterService])
    ], Counter1Component);
    return Counter1Component;
}());
exports.Counter1Component = Counter1Component;
//# sourceMappingURL=counter1.component.js.map