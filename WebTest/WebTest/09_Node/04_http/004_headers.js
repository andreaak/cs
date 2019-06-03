var http = require('http');

http.createServer(function(req, res){
    // возвращает массив c заголовками, четные элементы массива - имена заголовков, нечетные - значения заголовков
    console.log(req.rawHeaders);
    // setHeader - метод записывает в ответ указаный заголовок и его значение
    res.setHeader('Content-Type', 'text/plain');
    // 
    var ct = res.getHeader('Content-Type');
    console.log(ct);

    // свойство определяет будет ли сервр отправлять заголовок Date. Если значение свойства false - дата не отправляется
    res.sendDate = false;
    // 
    console.log(res.headersSent);

    res.end('<h1>Test page</h1>');
    
    console.log(res.headersSent);

}).listen(8080);