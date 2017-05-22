// Декоратор для класса
// Декоратор должен представлять функцию возвращающую новый конструктор

function testClassDecorator() {
   return (target) => { 

        let newConstructor : any = function() {
            target.apply(this); // выполняем конструктор класса для которого применен декоратор
            this.newProp = 123; // добавляем новое свойство
        }

        return newConstructor;
   };
}

@testClassDecorator() // применяем новый декоратор
class Test3 {
    prop1 = 10;
}

let t3 = new Test3();
console.log(t3);

