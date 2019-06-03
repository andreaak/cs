var express = require('express');
var app = express();

app.use('/', function (req, res) {

    console.log('Cookies from client:', req.headers['cookie']);

    // Метод позволяет записать в заголовок только один кукис
    //res.setHeader('Set-Cookie', 'TestHeader=HeaderValue');

    // Метод принимает имя заголовка - первым параметром и его значение - вторым параметром
    res.setHeader('Set-Cookie', ['item3=value3', 'item4=value4']);

    // Метод позволяет задать статус код ответа и объект с заголовками
    //res.writeHead(200, { 'Set-Cookie': ['item1=value1', 'item2=value2'] });

    console.log('Method getCookie():', res.getHeader('Set-Cookie'));
    // записываем в ответ содержиое файла
    res.sendFile(__dirname + '/index.html');
});

app.listen(8080, function () {
    console.log('Server start on port', 8080);
});

