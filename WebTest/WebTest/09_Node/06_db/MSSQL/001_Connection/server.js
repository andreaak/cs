var express  = require('express'); 
var app = express(); 

var port = 8080; 

var mssql = require('mssql'); 


// параметры соединения с бд
var config = {
	user: 'test',   				// пользователь базы данных
	password: '12345', 	 			// пароль пользователя 
	server: 'localhost', 			// хост
	database: 'testdb',    			// имя бд
	port: 1433,			 			// порт, на котором запущен sql server
    pool: {
        max: 10, 					// максимальное допустимое количество соединений пула 
        min: 0,  					// минимальное допустимое количество соединений пула 
        idleTimeoutMillis: 30000 	// время ожидания перед завершением неиспользуемого соединения 
    }	
}

app.use(function(req, res) { 
	// mssql.ConnectionPool - 
	var connection = new mssql.ConnectionPool(config); 

	connection.connect(function(err){		
		// mssql.Request - 
		var request = new mssql.Request(connection);  
		// request.query - 
		request.query('SELECT * FROM items', function(err, data) {
			if (err){
				console.log(err);
				return;
			}  
			else {				
				console.log(data); 
				res.send(data.recordset); 				
			}
		});		
	});
}); 

app.listen(port, function() { 
	console.log('app listening on port ' + port); 
}); 