// для работы с mongodb необходимо подключить модуль. Для этого используйте комманду - npm i mongodb
// MongoClient основной клас для работы с БД, через него происходят все взаимодействия с БД
var MongoClient = require('mongodb').MongoClient;
var format = require('util').format;

// Путь, по которому устанавливается соединение c БД, test - имя базы к которой подключаемся
var url = 'mongodb://localhost:27017/test';
// Используем метод connect для подключения к серверу, функция которая передается в метод, принимает два параетра 
    // err - ошибка, которая возникла при установке соединения с БД
    // db - ссылка на объект БД
MongoClient.connect(url, function(err, db) {
    if(err) throw err;

    console.log('Connection established!');
    // Закрываем подключение с БД
    db.close();
});