var http = require('http');
// Модуль cookies позволяет про
var Cookies = require('cookies');

http.createServer(function(req, res){
    // для работы с куками через модуль cookies необходимо создать объект куки
    var cookies = new Cookies(req, res);

    if(req.url == '/favicon.ico'){
        res.end();
        return;
    }
    
    cookies.set('admin', 'true');
    
    console.log(cookies.get('node'));

    res.end();

}).listen(8080, function(){
    console.log('Server start on port', 8080);
})

// Documentation - https://www.npmjs.com/package/cookies