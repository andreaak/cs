var evt = require('events').EventEmitter;
// создаем обьект события
var emt = new evt();

// Добавляем обработчик на событие myEvent
emt.on('myEvent', test);
console.log('Listener added!');

// Генерируем событие myEvent
emt.emit('myEvent');

// Удаляем обработчик с события myEvent
emt.removeListener('myEvent', test);
console.log('Listener removed!');

// Повторно генерируем событие myEvent
emt.emit('myEvent');

function test(){
    console.log('test function!');
}