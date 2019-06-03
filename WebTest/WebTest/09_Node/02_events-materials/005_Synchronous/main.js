// Обработчики, которые установленны на определенное событие, вызываются синхронно
var evt = require('events').EventEmitter;
// создаем обьект события
var emt = new evt();

emt.on('event', function(){
    console.log("Listener № 1");
});

emt.on('event', function(){
    console.log("Listener № 2");
});

emt.on('event', function(){
    console.log("Listener № 3");
});

// Генерируем событие myEvent и в функцию обработчик передаем 2 параметра
emt.emit('event');