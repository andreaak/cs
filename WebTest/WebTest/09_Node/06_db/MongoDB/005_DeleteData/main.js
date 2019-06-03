var MongoClient = require('mongodb').MongoClient;

var url = 'mongodb://localhost:27017/userCollectionDB';

// Для удаления объектов (документов) из коллекции используется используется несколько методов
    // deleteMany()         - удаляет все документы, которые соответствуют определенному критерию
    // deleteOne()          - удаляет один документ, который соответствует определенному критерию
    // findOneAndDelete()   - получает и удаляет один документ, который соответствует определенному критерию
    // drop()               - удаляет всю коллекцию

MongoClient.connect(url, function(err, db){
    var collection = db.collection('users');

    collection.find().toArray(function(err, res){
        console.log(res);
    });

    // collection.deleteOne({name : "Sergey", age: 37}, function(err, result){
    //     console.log(result);

    //     collection.find().toArray(function(err, res){
    //         console.log(res);
    //         db.close();
    //     });
    // });    
        
    collection.deleteMany({name: "Ivan"}, function(err, result){             
        console.log(result);
        db.close();
    });
});