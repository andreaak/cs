var MongoClient = require('mongodb').MongoClient;

var url = "mongodb://localhost:27017/userCollectionDB";

MongoClient.connect(url, function(err, db){
    // в mongoDB нет таблиц, все данные хранятся в коллекциях, и для того что бы работать с БД необходимо получить объект коллекции
    // метод collection применяется для получения объекта коллекции
    var collection = db.collection('users');

    var user = {firstName : "Ivan", lastName : "Ivanov", age: 30};

    // метод insertOne позволяет добавить новый объект (документ) в коллекцию, принимает два параметра:
        // 1-й - объект оторый необходимо добавить
        // 2-й - функция обратного вызова которая выполнится после добавления объекта
    collection.insertOne(user, function(err, result){
         
        if(err){ 
            console.log(err);
            return;
        }
        // result.ops - объект полученный обратно из базы данных, содержащий идентификатор
        console.log(result.ops);
        db.close();
    });
});