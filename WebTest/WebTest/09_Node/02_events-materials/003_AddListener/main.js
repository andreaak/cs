'use strict'

var count = 0;
var print = () => {
    count++;
    console.log('Click - ', count);
}

// Для работы с событиями, необходимо подключить модуль 'events'
var evt = require('events');

var emt = new evt.EventEmitter;

// метод on и addListener добавляют обработчик на событие
emt.on('click', print);
emt.addListener('click', print);

// добавив обработчик с помощью метода once, обработчик сработает только один раз и будет удален
emt.once('click', print);

// Вызов обработчиков 
emt.emit('click');
emt.emit('click');

