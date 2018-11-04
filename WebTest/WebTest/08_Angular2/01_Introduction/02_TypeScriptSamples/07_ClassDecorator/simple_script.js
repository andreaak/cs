var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
function testClassDecorator() {
    return function (target) {
        var newConstructor = function () {
            target.apply(this);
            this.newProp = 123;
        };
        return newConstructor;
    };
}
var Test3 = (function () {
    function Test3() {
        this.prop1 = 10;
    }
    Test3 = __decorate([
        testClassDecorator(), 
        __metadata('design:paramtypes', [])
    ], Test3);
    return Test3;
}());
var t3 = new Test3();
console.log(t3);
//# sourceMappingURL=simple_script.js.map