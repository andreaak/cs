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
// OpaqueToken может использоваться, когда создаваемая зависимость не является классом.
// При создании OpaqueToken в конструкторе в качестве параметра указывается строковое описание токена.
// Для того, чтобы внедрить связанное значение с этим токеном необходимо использовать декоратор @Inject
var APP_CONFIG = new core_1.OpaqueToken("app_config"); // в конструктор передается описание
var config = {
    prop1: "value 1",
    prop2: "value 2"
};
var Sample5Component = (function () {
    // При создании Sample5Component инжектор внедрит значение объекта config в аргумент конструктора с именем config
    function Sample5Component(config) {
        this.config = config;
    }
    Sample5Component.prototype.logConfigIngo = function () {
        console.log(this.config.prop1);
        console.log(this.config.prop2);
    };
    Sample5Component = __decorate([
        core_1.Component({
            moduleId: module.id,
            selector: "my-sample5",
            templateUrl: "sample5.component.html",
            providers: [{
                    provide: APP_CONFIG,
                    useValue: config
                }]
        }),
        __param(0, core_1.Inject(APP_CONFIG)), 
        __metadata('design:paramtypes', [Object])
    ], Sample5Component);
    return Sample5Component;
}());
exports.Sample5Component = Sample5Component;
//# sourceMappingURL=sample5.component.js.map