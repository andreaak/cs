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
// Свойства, которые будут устанавливаться
var CounterComponent = (function () {
    function CounterComponent() {
        this.name = "default name"; // данное свойство можно задать с помощью кода <counter name="Counter 1"></counter>
        this.counterValue = 0; // данное свойство можно задать с помощью кода <counter counterValue="10"></counter>
        this.counterStep = 1; // данное свойство можно задать с помощью кода <counter step="2"></counter>
    }
    CounterComponent.prototype.increment = function () {
        this.counterValue = this.counterValue + this.counterStep;
    };
    __decorate([
        // данное свойство можно задать с помощью кода <counter name="Counter 1"></counter>
        core_1.Input(), 
        __metadata('design:type', Number)
    ], CounterComponent.prototype, "counterValue", void 0);
    __decorate([
        // данное свойство можно задать с помощью кода <counter counterValue="10"></counter>
        core_1.Input("step"), 
        __metadata('design:type', Number)
    ], CounterComponent.prototype, "counterStep", void 0);
    CounterComponent = __decorate([
        core_1.Component({
            moduleId: module.id,
            selector: "counter",
            templateUrl: "counter.component.html",
            inputs: ["name"]
        }), 
        __metadata('design:paramtypes', [])
    ], CounterComponent);
    return CounterComponent;
}());
exports.CounterComponent = CounterComponent;
//# sourceMappingURL=counter.component.js.map