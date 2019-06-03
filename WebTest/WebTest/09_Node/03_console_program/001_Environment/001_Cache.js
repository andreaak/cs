require('./test.js');
require('./simple.js');

console.log('Main file is loaded!');

console.log('');

console.log(require.cache); // свойство cache объекта require, хранит закешированные модули