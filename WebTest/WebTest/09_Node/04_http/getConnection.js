var http = require('http');

var port = 8080;

console.log('Server running on port ' + port); 

    // Создание запроса 
    // Параметры создаваемого запроса 
var config = {
    host: 'localhost',
    port: port,
    path: '/testConnection'
};

// Для создания запроса используется метод http.request(), который принимает в качестве аргумента объект конфигурации запроса
var req = http.request(config, function (response) {

    // Записывать данные в body по мере поступления 
    var body = '';
    response.on('data', function (data) {
         body += data;
    });

    response.on('end', function () {
        // Данные полностью получены 
        console.log(body);
    });
});

req.end();

