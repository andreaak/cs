var http = require('http'); 
var port = 8080;

// сервер для обработки запроса 
var server = http.createServer(function (req, res) { 

    console.log('request')
    res.end('GET request path: ' + req.url);  

}).listen(port); 
