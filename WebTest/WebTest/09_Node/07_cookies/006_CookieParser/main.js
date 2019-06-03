var express = require('express');
var cookieParser = require('cookie-parser');

var app = express();

app.use(cookieParser('Secret string'));

app.get('/', function(req, res){
    // Установка куков
    res.cookie('login', 'admin', { maxAge : 10000 });
    
    console.log(req.cookies);

    // Удаление куков
    res.clearCookie('login');

    res.end();
});

app.listen(8080);

// Documentation - https://www.npmjs.com/package/cookie-parser