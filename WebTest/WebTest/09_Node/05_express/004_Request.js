var express = require('express'); 
var app = express(); 

var port = 8080; 
// метод all позволяет установить обработчик на маршруты, при обращении к которым протокол не имеет значения
app.all('*', function(req, res) { 
 
    // http метод => GET 
	console.log('method: ' + req.method); 
    // параметры адресной строки в виде объекта 
	console.log('query: ' + req.query); 
    // протокол (http или https) 
	console.log('protocol: ' + req.protocol); 
    // true или false(true если req.protocol == 'https')
	console.log('secure: ' + req.secure);  

	// req.accepts - метод, который проверяет типы данных (MIME type), которые допустимы  
	// для данного запроса. Если тип данных недопустим, возвращает false 
	console.log('accepts: ' + req.accepts(['text/html', 'application/json'])); 
    // возвращает указанный заголовок запроса 
    console.log('content type header: ' + req.get('Content-Type')); 

    console.log('------------------------');
	res.end(); 
}); 

app.listen(port, function() {
	console.log('app running on port ' + port); 
}); 