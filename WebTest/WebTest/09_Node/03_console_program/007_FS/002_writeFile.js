// Модуль fs предназначен для работы с файлами. методы содержащиеся в модуле имеют синхронную и асинхронную формы

var fs = require('fs');
var utils = require('util');

user = {
    fname : "Ivan",
    lname : "Ivanov",
    age : 30,
    position : "developer"
}
console.log('File writing...');
fs.writeFile('text.txt', utils.format('%j', user), function(err){
    if(err){
        console.log(err);
        return;
    }
    console.log('File was wrote!');
});

