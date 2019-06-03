var http = require('http');
var port = 8080;

var options = {
	port: port, 
	host: 'localhost', 
	method: 'POST', 
	path: '/?a=1',  
	headers: {'Content-Type':'text/html'}

};

http.get(options);  
