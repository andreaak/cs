var express = require('express'); 
var app = express(); 

var path = require('path'); 
var port = 8080;  

// multer - middleware для обработки данных формы в кодировке multipart/form-data 
var multer = require('multer');   

var upload = multer();  

app.get('/', function(req, res) {
	res.sendFile(path.join(__dirname, 'index.html')); 
})

// конфигурация модуля multer 
var form_data = upload.fields([{name: 'username'}, {name: 'email'}]); 

app.post('/form_handler', form_data, function(req, res) {	
	// при использовании модуля multer данные полей формы доступны через объект req.body  	
	console.log(req.body); 
	res.send(`<h2>user data:</h2> <p>username: ${req.body.username}</p> <p>email: ${req.body.email}</p>`);	
 })
 
 app.listen(port, function() {
	    console.log('App listening on port ' + port);
 }); 

