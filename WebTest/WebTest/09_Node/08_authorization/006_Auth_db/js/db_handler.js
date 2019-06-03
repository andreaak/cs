var mssql = require('mssql');

var config = {
	user: 'test',   // пользователь базы данных
	password: '12345', 	 // пароль пользователя 
	server: 'localhost', // хост
	database: 'testdb',    // имя бд
	port: 1433,			 // порт, на котором запущен sql server
	pool: {
		max: 10, // максимальное допустимое количество соединений пула 
		min: 0,  // минимальное допустимое количество соединений пула 
		idleTimeoutMillis: 30000 // время ожидания перед завершением неиспользуемого соединения 
	}
};


var connection = new mssql.Connection(config);
var pool = connection.connect(function (err) {
	if (err) console.log(err)
});

module.exports = pool; 