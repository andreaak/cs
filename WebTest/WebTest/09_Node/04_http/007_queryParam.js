const http = require('http'); 
const url = require('url'); 

const server = http.createServer(); 

var port = 8080;

server.on('request', function(req, res) { 
    var method = req.method;
    var _url = req.url;

	console.log('Method:', method, '; URL:', _url); 

	var parsed = url.parse(req.url, true); 
	console.log(parsed);
    
	if (parsed.pathname == '/test' && parsed.query.message) {

		res.statusCode = 200; // OK

		res.end(parsed.query.message); 
	} 
	else {
		res.statusCode = 404; // Not Found
		res.end('Page not found on server!'); 
	}
}); 

server.listen(port); 

server.on('listening', function() {
	console.log('Server running on port ' + port); 
}); 