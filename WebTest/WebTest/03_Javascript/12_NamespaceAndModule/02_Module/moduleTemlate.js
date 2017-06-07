// определяем пространство имен
App.define("util.calc");

// Определение модуля
// инициализируем объект используя немелденно вызываемую функцию.
App.util.calc = (function () {
    // закрытые члены
    var x, y;

    return {
        // открытые члены
        add: function () {
            return x + y;
        },
        setX: function (val) {
            x = val;
        },
        setY: function (val) {
            y = val;
        }
    }
}());