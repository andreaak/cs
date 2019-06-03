// Для удаления объектов (документов) из коллекции используется используется несколько методов
    // updateOne: обновляет один документ, который соответствует критерию фильтрации, и возвращает информацию об операции обновления
    // updateMany: обновляет все документы, которые соответствуют критерию фильтрации, и возвращает информацию об операции обновления
    // findOneAndUpdate: обновляет один документ, который соответствует критерию фильтрации, и возвращает обновленный документ

var mongoClient = require("mongodb").MongoClient;
 
var users = [{name: "Bob", age: 34} , {name: "Alice", age: 21}, {name: "Tom", age: 45}];
 
mongoClient.connect("mongodb://localhost:27017/userCollectionDB", function(err, db){
     
    var collection = db.collection("users");
    collection.insertMany(users, function(err, results){
             
        collection.findOneAndUpdate(
            // критерий выборки
            {age: 25}, 
            // параметр обновления
            { $set: {age: 21}}, 
            // доп. опции обновления   
            {                            
                 returnOriginal: false
            },
            function(err, result){                 
                console.log(result);
                db.close();
            }
        );
    });
});