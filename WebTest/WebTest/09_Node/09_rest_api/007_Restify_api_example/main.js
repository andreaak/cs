var restify = require('restify');
var fs = require('fs'); 

var port = 8080; 
 
// модуль для обработки запросов 
var apiHandler = require('./handler'); 

// создание сервера 
var server = restify.createServer({
    name: 'myApp'
});

server.use(function(req, res, next) {
    console.log(req.method + ' ' + req.url); 
    next(); 
})

server.get('/', function (req, res) {
    res.redirect('/api');
})

// выбрать все элементы 
server.get('/api', apiHandler.getItems);

// создать новый элемент 
server.post('/api', apiHandler.addItem);

// обновить элемент по ID 
server.put('/api/:id', apiHandler.updateItem);

// удалить элемент по ID 
server.del('/api/:id', apiHandler.removeItem);  
 
// обработчик ошибок 
server.on('InternalServer', function(err) {
    err.body = 'oops...error'; 
    res.send(err); 
})

server.listen(port, function () {
    console.log('server running on port ' + port);
});