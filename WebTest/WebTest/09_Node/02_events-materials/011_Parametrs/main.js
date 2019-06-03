var evt = require('events').EventEmitter;
// создаем обьект события
var emt = new evt();

emt.on('myEvent', function(a, b){
    console.log(a, b)
});

// Генерируем событие myEvent и в функцию обработчик передаем 2 параметра
emt.emit('myEvent', 10, 15);