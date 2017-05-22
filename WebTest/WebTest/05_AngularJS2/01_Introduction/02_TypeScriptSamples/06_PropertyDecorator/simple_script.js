// NOTE: Декораторы - экспериментальная функциональность, которая может измениться в будущем
// для того чтобы пример работал, в настройках компилятора нужно указать параметры experimentalDecorators и emitDecoratorMetadata
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
// Декораторы дают возможность использовать аннотации и добавлять метаданные к классам и их членам.
// Декоратор Override - позволяет заменить значение свойства на указанное в параметре
function Override(label) {
    // target - объект в котором используется декоратор
    // key - имя свойства
    return function (target, key) {
        Object.defineProperty(target, key, {
            configurable: false,
            get: function () { return label; }
        });
    };
}
var Test = (function () {
    function Test() {
        this.name = "...";
    }
    __decorate([
        Override("Hello world"), 
        __metadata('design:type', String)
    ], Test.prototype, "name", void 0);
    return Test;
}());
var t = new Test();
console.log(t.name); // Hello world
// ReadOnly - указывает, что свойство не может менять значение.
function ReadOnly(target, key) {
    Object.defineProperty(target, key, { writable: false });
}
var Test2 = (function () {
    function Test2() {
    }
    __decorate([
        ReadOnly, 
        __metadata('design:type', String)
    ], Test2.prototype, "name", void 0);
    return Test2;
}());
var t2 = new Test2();
t2.name = "Ivan";
console.log(t2.name); // undefined
//# sourceMappingURL=simple_script.js.map