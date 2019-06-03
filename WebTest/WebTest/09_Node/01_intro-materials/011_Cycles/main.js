console.log('Main module is starting.');

var a = require('./testA.js');
var b = require('./testB.js');

console.log('in main, testA.done=', a.done, ' testB.done=' , b.done);