var http = require("http");

var server = http.createServer(function(request, response) {
    // writeHead - метод позволяет записать в ответ заголовки и статус код
    response.writeHead(200, {"Content-Type": "text/plain"});
    // write - метод позволяет создать тело ответа в виде потока writeable Stream
    response.write("Hello World");
    // end - завершает конфигурациюответа и отправляет его
    response.end();

}).listen(8080, function(){
    console.log('Server running on port 8080');
});
