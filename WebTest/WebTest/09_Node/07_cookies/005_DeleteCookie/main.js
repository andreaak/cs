var http = require('http');
var Cookies = require('cookies');

http.createServer(function(req, res){
    // для работы с куками через модуль cookies необходимо создать объект куки
    var cookies = new Cookies(req, res);
   
    // Используя модуль cookies можно конфигурировать куки дополнительными параметрами, для этого в метод set нужно передать дополнительный обьект с опциями
    cookies.set('login', 'test@ex.org',  { maxAge : -1, path: '/admin' });

    res.end();

}).listen(8080, function(){
    console.log('Server start on port', 8080);
})