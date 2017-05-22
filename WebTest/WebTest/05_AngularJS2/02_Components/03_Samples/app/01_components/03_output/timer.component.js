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
var TimerComponent = (function () {
    function TimerComponent() {
        this.currentValue = 0;
        this.interval = 1000;
        this.tick = new core_1.EventEmitter();
    }
    TimerComponent.prototype.start = function () {
        var _this = this;
        if (this.intervalObject)
            return;
        this.intervalObject = setInterval(function () { _this.callback(); }, this.interval);
    };
    TimerComponent.prototype.stop = function () {
        if (!this.intervalObject)
            return;
        clearInterval(this.intervalObject);
        this.intervalObject = false;
    };
    TimerComponent.prototype.callback = function () {
        this.currentValue++;
        this.tick.emit(this.currentValue);
    };
    __decorate([
        core_1.Input(), 
        __metadata('design:type', Number)
    ], TimerComponent.prototype, "interval", void 0);
    __decorate([
        core_1.Output(), 
        __metadata('design:type', core_1.EventEmitter)
    ], TimerComponent.prototype, "tick", void 0);
    TimerComponent = __decorate([
        core_1.Component({
            moduleId: module.id,
            selector: "timer",
            templateUrl: "timer.component.html"
        }), 
        __metadata('design:paramtypes', [])
    ], TimerComponent);
    return TimerComponent;
}());
exports.TimerComponent = TimerComponent;
//# sourceMappingURL=timer.component.js.map