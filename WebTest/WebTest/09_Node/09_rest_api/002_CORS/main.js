var express = require('express');
var app = express(); 
var router = express.Router(); 

var port = 8080; 

var router = require('./route')

// middleware для использования CORS  
app.use(function (req, res, next) {
    res.header("Access-Control-Allow-Origin", "*");
    res.header("Access-Control-Allow-Headers", "Origin, X-Requested-With, Content-Type, Accept");
    next();
});


// использовать роутер на пути /api
app.use('/api', router.rout); 

app.listen(port, function () {
    console.log('app running on port ' + port);
});