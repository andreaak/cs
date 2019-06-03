var express = require('express');
var passport = require('passport');
var localStrategy = require('passport-local');
var http = require('http');
var session = require('express-session');
var bodyParser = require('body-parser');
var cookieParser = require('cookie-parser');

var app = express();

app.use(passport.initialize());
app.use(passport.session());
app.use(express.static(__dirname + '/public'));
app.use(session({
    resave: false,
    saveUninitialized: false,
    secret: 'secret string'
}));
app.use(cookieParser('some text'));
app.use(bodyParser.json());
app.set('views', __dirname + '/views');
app.set('view engine', 'ejs');


// создаем функцию для проверки аутентификации пользователя
function ensureAuth(req, res, next) {
    if (req.isAuthenticated()) {
        return next();
    }
    res.redirect('/login');
}

// функция для сериализации пользовательских данных для сеанса
passport.serializeUser(function (user, done) {
    done(null, user.id);
});

// функция десериализации данных
passport.deserializeUser(function (user, done) {
    done(null, { username: id });
});

// Настраиваем локальную стратегию
passport.use(new localStrategy(function (user, password, done) {
    if (username != 'admin') {
        return done(null, false, { message: 'Wrong login!' });
    };

    if (password != '12345') {
        return done(null, false, { message: 'Wrong password!' });
    };

    return done(null, { username: 'admin' });
}));

app.get('/', function (req, res) {
    res.render('index', { user: req.user });
});

app.get('/admin', ensureAuth, function (req, res) {
    res.render('admin', { user: req.user });
});

app.get('/login', function (req, res) {
    var username = req.user ? req.user.username : '';

    res.render('login', { user: req.user });
});

app.post('/login', passport.authenticate('local', { failureRedirect: '/login', falureFlash: true }), function (req, res) {
    res.redirect('/admin');
});

http.createServer(app).listen(8080);

console.log('server started!')

