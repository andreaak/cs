// Определение класса Rectangle реализующего интерфейс Shape
var Rectangle = (function () {
    function Rectangle(h, w) {
        this.name = "Rectangle";
        this.height = h;
        this.width = w;
    }
    Rectangle.prototype.printArea = function () {
        var area = this.height * this.width;
        console.log("Area of " + this.name + " is " + area);
    };
    return Rectangle;
}());
// Определение класса Circle реализующего интерфейс Shape
var Circle = (function () {
    function Circle(r) {
        this.name = "Circle";
        this.radius = r;
    }
    Circle.prototype.printArea = function () {
        var area = Math.PI * Math.pow(this.radius, 2);
        console.log("Area of " + this.name + " is " + area);
    };
    return Circle;
}());
// Определение массива Shape
var shapes = new Array();
// В данном массиве могут находиться только те объекты, которые имеют метод printArea и свойство name
shapes[0] = new Rectangle(100, 200);
shapes[1] = new Circle(20);
for (var i = 0; i < shapes.length; i++) {
    var currentShape = shapes[i];
    currentShape.printArea();
}
//# sourceMappingURL=simple_script.js.map