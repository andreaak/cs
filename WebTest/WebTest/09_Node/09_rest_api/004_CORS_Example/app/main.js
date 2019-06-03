var express = require('express');
var app = express();  

var http = require('http'); 
var path = require('path'); 

var bodyParser = require('body-parser'); 

app.use(bodyParser.json()); 

var port = 8081; 
var portApi = 8080;

app.get('/', function (req, res) {
    res.sendFile(path.join(__dirname, 'index.html')); 
}); 

var cb = function (response, res) {
    response.on('data', function (chunk) {
       
        res.write(chunk.toString());
    });

    response.on('end', function () {
        res.end();
    })
};

app.get('/all', function (req, res) {

    var options = {
        host: 'localhost', 
        port: portApi, 
        method: 'GET', 
        path: '/api/'

    } 

    var request = http.request(options, function (response) {
        cb(response)
    }); 
    request.end(); 
}); 

app.get('/view/:id', function (req, res) {

     var options = {
        host: 'localhost',
        port: portApi, 
        method: 'GET',
        path: '/api/' + req.params.id
    }

     var request = http.request(options, function (response) {
         cb(response)
     });
     request.end(); 
}); 

app.post('/new', function (req, res) {

     var options = {
        host: 'localhost',
        port: portApi, 
        method: 'POST',
		path: '/api/new' ,
		headers: {
            'Content-Type': 'application/json'
        }
    }

     var request = http.request(options, function (response) {
         cb(response)
     });
     request.write(JSON.stringify(req.body));
     request.end();     
}); 

app.post('/edit/:id', function (req, res) {

     var options = {
        host: 'localhost',
        port: portApi, 
        method: 'PUT',
        path: '/api/' + req.params.id, 
        headers: {
            'Content-Type': 'application/json'
        }
    }

     var request = http.request(options, function (response) {
         cb(response)
     }); 
     console.log('POST BODY:', req.body);
     request.write(JSON.stringify(req.body));
     request.end();     
});

app.get('/delete/:id', function (req, res) {

     var options = {
        host: 'localhost',
        port: 1337, 
        method: 'DELETE', 
        path: '/api/' + req.params.id 
    }

     var request = http.request(options, function (response) {
         cb(response, res)
     });
     request.end(); 
});

app.listen(port, function() {
	
	console.log('app running on port ' + port); 
})