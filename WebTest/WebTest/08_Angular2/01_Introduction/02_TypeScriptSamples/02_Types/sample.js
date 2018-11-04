// Определение переменной;
// let идентификатор: тип = значение;
var isDone = true; // true/false
var value = 10; // целочисленные, вещественные Infinity и NaN
var stringValue = "test"; // строковые значения
var list1 = [1, 2, 3]; // определение массива чисел
var list2 = [1, 2, 3]; // определение массива числе с использования generic типа Array<elemType>
// определение перечисления
// перечисление дают возможность задать понятные имена для набора числовых значений
var Color;
(function (Color) {
    Color[Color["Red"] = 1] = "Red";
    Color[Color["Green"] = 2] = "Green";
    Color[Color["Blue"] = 3] = "Blue";
})(Color || (Color = {}));
;
var c = Color.Green; // определение переменной с типа перечисления Color
// any - тип для определения переменной типа которой в данный момент мы не знаем.
var someValue = 4;
someValue = "test string";
someValue = false;
// определение функции которая принимает аргумент типа string и ничего не возвращает
function showMessage(data) {
    alert(data);
}
showMessage('hello');
//# sourceMappingURL=sample.js.map