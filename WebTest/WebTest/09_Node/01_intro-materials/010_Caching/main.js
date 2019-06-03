// В node.js при первом подключении модуля происходит выполнение кода в подключаемом модуле, и модуль помещается в кеш
// при повторном подключени того же модуля, среда проверяет путь к подключаемому файлу, если в кеше уже есть такой путь, то возвразается уже существующий объект

var user = require('./cache')

user.sayHello();

require('./sample');

user.sayHello();
// Hello!  My name is  Ivan  I'm  25  years old!
// Hello!  My name is  Sergey  I'm  30  years old!