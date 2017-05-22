"use strict";
var Car = (function () {
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
///////////////////////////////////////////
// BAD PRACTICE
//////////////////////////////////////////
// Сопровождение такого класса будет усложнятся по мере роста приложения
// Для решения подобных проблем используется Dependency Injection Framework
var CarFactory = (function () {
    function CarFactory() {
    }
    CarFactory.prototype.createCar = function () {
        var car = new Car(this.createEngine(), this.createTires());
        return car;
    };
    CarFactory.prototype.createEngine = function () {
        return new Engine();
    };
    CarFactory.prototype.createTires = function () {
        return new Tires();
    };
    return CarFactory;
}());
//# sourceMappingURL=03_factory.js.map