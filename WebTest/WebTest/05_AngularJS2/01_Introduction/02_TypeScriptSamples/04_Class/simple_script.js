var Human = (function () {
    function Human(nameValue, ageValue, selector) {
        this.name = nameValue;
        this.age = ageValue;
        this.messageElementId = document.querySelector(selector);
    }
    Human.prototype.say = function () {
        var message = "My name is " + this.name + " I am " + this.age + " years old";
        this.messageElementId.innerHTML = message;
    };
    return Human;
}());
var h = new Human("Ivan", 25, "#output");
h.say();
//# sourceMappingURL=simple_script.js.map