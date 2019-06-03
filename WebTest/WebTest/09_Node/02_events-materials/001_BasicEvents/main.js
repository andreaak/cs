// Для работы с событиями, необходимо подключить модуль 'events'
var evt = require('events');

// Все обьекты которые генерируют события в Node.js должны быть экземплярами класса EventEmitter, или его наследников
var emitter = new evt.EventEmitter();

// Что бы установить слушателя на событие, необходимо воспользоваться методом  on(), который доступен на обьекте события
 emitter.on('create', function(){
     console.log('Folder was created!');    
 });
 
// Метод emit предназначен для генерации события 
 emitter.emit('create');

