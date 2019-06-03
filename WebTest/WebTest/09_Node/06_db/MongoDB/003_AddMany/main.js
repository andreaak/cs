var MongoClient = require('mongodb').MongoClient;
var persons = require('./persons');


var url = 'mongodb://localhost:27017/userCollectionDB';

MongoClient.connect(url, function(err, db){
    var collection = db.collection('users');
    // метод insertMany используется для добавления множества обьектов
    collection.insertMany(persons, function(err, results){
        if(err) throw err;

        console.log('Data added!');
        console.log('********** Result **********');
        console.log(results);
        console.log('****************************');        
        db.close();
    });    
});