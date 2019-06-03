var evt = require('events').EventEmitter;
// создаем обьект события
var emt = new evt();

// Когда вызывается функция обработчик, ключевое слово this внутри нее указывает на объект EventEmitter,
// от этого можно избавиться используя стрелочную функцию
emt.on('myEvent', function(){
    console.log('Ordinary function: ');
    console.log(this);
});

module.exports.x = 10;
emt.on('myEvent', () => {
    console.log('Arrow function: ')
    console.log(this)
});

// Генерируем событие myEvent и в функцию обработчик передаем 2 параметра
emt.emit('myEvent');
/*
Ordinary function:
EventEmitter {
  _events:
   [Object: null prototype] { myEvent: [ [Function], [Function] ] },
  _eventsCount: 1,
  _maxListeners: undefined }
Arrow function:
{ x: 10 }
*/