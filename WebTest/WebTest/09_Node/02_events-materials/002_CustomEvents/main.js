// Для работы с событиями, необходимо подключить модуль 'events'
var fileReader = require('./readFile.js');
// Создаем объект ридера
var reader = new fileReader.Reader();
// функция readDataFromFile подключается к файлу, считывает данные и передает в нашу callback функцию
reader.readDataFromFile('file.js', function(responce){
    console.log(responce);
});
