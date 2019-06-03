// Для работы с событиями, необходимо подключить модуль 'events'
var evt = require('events');

var emt = new evt.EventEmitter;

emt.on('myEvent', function(){
    console.log('test string 1');
});
// Метод prependListener добавляет подписчика в начало цепочки
emt.prependListener('myEvent', function(){
    console.log('test string 2');
});

emt.once('once', function(){
    console.log('First once listener');
});
// Метод prependListener добавляет подписчика в начало цепочки
emt.prependOnceListener('once', function(){
    console.log('Seconds once listener');
});

emt.emit('myEvent');
emt.emit('once');


