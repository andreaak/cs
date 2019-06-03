var evt = require('events').EventEmitter;
// создаем обьект события
var emt = new evt();

emt.on('myEvent', function(){
    console.log('First listener.');
});

emt.on('myEvent', function(){
    console.log('Second listener.');
});
// метод listeners возвращает массив функций обработчиков для указаного события
var listeners = emt.listeners('myEvent');

for (var index = 0; index < listeners.length; index++) {
    listeners[index]();
}