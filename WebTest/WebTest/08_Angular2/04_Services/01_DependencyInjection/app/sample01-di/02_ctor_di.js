"use strict";
var __extends = (this && this.__extends) || function (d, b) {
    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
    function __() { this.constructor = d; }
    d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
};
var Car = (function () {
    // При вызове данного конструктора, необходимо передать два параметра,
    // которые станут закрытыми полями класса Car
    function Car(engine, tires) {
        this.engine = engine;
        this.tires = tires;
    }
    Car.prototype.drive = function () {
        return "\u0410\u0432\u0442\u043E\u043C\u043E\u0431\u0438\u043B\u044C \u0441 \u0434\u0432\u0438\u0433\u0430\u0442\u0435\u043B\u0435\u043C " + this.engine.cylinders + " \u0446\u0438\u043B\u0438\u043D\u0434\u0440\u043E\u0432 \u0438 \u0440\u0435\u0437\u0438\u043D\u043E\u0439 " + this.tires.name;
    };
    return Car;
}());
exports.Car = Car;
var Engine = (function () {
    function Engine() {
    }
    return Engine;
}());
var Engine2 = (function () {
    function Engine2() {
    }
    return Engine2;
}());
var Tires = (function () {
    function Tires() {
    }
    return Tires;
}());
// зависимости передаются при создании экземпляра класса.
var c1 = new Car(new Engine(), new Tires());
// это дает возможность использовать другие классы зависимостей без внесения изменений в класс Car
var c2 = new Car(new Engine2(), new Tires());
// также появляется возможность тестировать класс Car
var MockEngine = (function (_super) {
    __extends(MockEngine, _super);
    function MockEngine() {
        _super.apply(this, arguments);
        this.cylinders = 8;
    }
    return MockEngine;
}(Engine));
var MockTires = (function (_super) {
    __extends(MockTires, _super);
    function MockTires() {
        _super.apply(this, arguments);
        this.name = "test";
    }
    return MockTires;
}(Tires));
var c3 = new Car(new MockEngine(), new MockTires());
// Но теперь появляется проблема с клиентами класса Car
// Для того чтобы создать экземпляр класса необходимо создать и настроить все зависимости. 
//# sourceMappingURL=02_ctor_di.js.map