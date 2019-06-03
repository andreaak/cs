var express = require('express'); 
var app = express(); 

var port = 8080; 

var apiHandler = require('./api'); 

app.use(function (req, res, next) {
    console.time(req.method + ' ' + req.url); 
    next(); 
}); 

app.get('/api', apiHandler.loadItems); 

app.post('/api/new', apiHandler.createItem); 

app.delete('/api/:id', apiHandler.removeItem);  

app.get('/', function (req, res) {
    res.send('test get request'); 
    console.timeEnd(req.method + ' ' + req.url); 
}); 

app.listen(port, function () {
    console.log('app running on port ' + port);
});