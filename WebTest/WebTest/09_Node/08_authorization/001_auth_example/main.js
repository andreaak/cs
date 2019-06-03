var express = require('express');
var app = express();

var cookieParser = require('cookie-parser');
var session = require('express-session');
var bodyParser = require('body-parser');
var path = require('path');

app.use(bodyParser.json());

var port = 8080;

// зарегистрированные пользователи, которые могут быть авторизованы
var users = [
	{ username: 'admin', password: '12345' },
	{ username: 'foo', password: 'bar' },
	{ username: 'user', password: 'test' }
];

// создание хранилища для сессий 
var sessionHandler = require('./js/session_handler');
var store = sessionHandler.createStore();

// регистрируем промежуточный обработчик, что бы парсить кукисы 
app.use(cookieParser());
// создание сессии
app.use(session({
	store: store,
	resave: false,
	saveUninitialized: true,
	secret: 'supersecret'
}));

app.get('/', function (req, res) {
	res.sendFile(path.join(__dirname, 'index.html'));
});

app.post('/login', function (req, res) {

	var foundUser;
	// поиск пользователя в массиве users 
	for (var i = 0; i < users.length; i++) {
		var u = users[i];
		if (u.username == req.body.username && u.password == req.body.password) {
			foundUser = u.username;
			break;
		}
	}
	if (foundUser !== undefined) {
		req.session.username = foundUser;
		console.log("Login succeeded: ", req.session.username);
		res.send('Login successful: ' + 'sessionID: ' + req.session.id + '; user: ' + req.session.username);
	} else {
		console.log("Login failed: ", req.body.username)
		res.status(401).send('Login error');
	}
});

app.get('/check', function (req, res) {
	if (req.session.username) {
		res.set('Content-Type', 'text/html');
		res.send('<h2>User ' + req.session.username + ' is logged in! </h2>')
	} else {
		res.send('not logged in');
	}
});

app.listen(port, function () {
	console.log('app running on port ' + port);
});
