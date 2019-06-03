var express = require('express');
var app = express();

var port = 8080; 

var session = require('express-session');

// подключение модуля connect-mssql
var MSSQLStore = require('connect-mssql')(session);

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
} 


// создание хранилища для сессии 
app.use(session({
    store: new MSSQLStore(config), 
	resave: false,
    saveUninitialized: true,
    secret: 'supersecret'
})); 


app.get('/', function (req, res) {

    console.log(req.session);

    req.session.numberOfRequests = req.session.numberOfRequests + 1;

    var requestCount = () => {
        return isNaN(req.session.numberOfRequests) ? 0 : req.session.numberOfRequests;
    };
    
    res.end('Number of reguests: ' + requestCount() +
        ' \n\r Refresh the page to increase count');
})

app.listen(port, function () {
    console.log('app running on port ' + port);
})