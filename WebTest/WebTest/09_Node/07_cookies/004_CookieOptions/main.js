var http = require('http');
var Cookies = require('cookies');

http.createServer(function (req, res) {
    // для работы с куками через модуль cookies необходимо создать объект куки
    var cookies = new Cookies(req, res, { "keys": ['Secret_string'] });

    console.log(cookies.get('login', { signed: true }));
    
    var cookieOprtions = {
        // свойство задает время жизни куке
        maxAge: 12000,
        // дата когда куки будет прострочен (браузер автоматически его удалит)
        // expires : (Date.now() + 7).toLocaleString(),
        // По умолчинию '/' это значит что кука видима на уровне всего приложения
        path: '/admin',
        // указывает что кука должна передаваться по https, если значение true
        secure: false,
        // Подпись кукиса
        signed: true // изменить на true     
    };

    // Используя модуль cookies можно конфигурировать куки дополнительными параметрами, для этого в метод set нужно передать дополнительный обьект с опциями
    cookies.set('login', 'test@ex.com', cookieOprtions);

    res.end();

}).listen(8080, function () {
    console.log('Server start on port', 8080);
})