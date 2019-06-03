var http = require('http');

http.createServer(function (req, res) {
  
    // формирование разных ответов сервера для запросов с различными HTTP методами 
    switch (req.method) {
        case 'GET': {
    
            var response_text = 'GET request to path ' + req.url

            console.log(response_text);
            res.end(response_text);          
            
            break;
        }
        case 'POST': {

            var response_text = 'POST request to path ' + req.url

            console.log(response_text);
            res.end(response_text) 
            break;
        }
    }

}).listen(8080);