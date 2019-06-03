var http = require('http');
var url = require('url');
var fs = require('fs');

http.createServer(function(req, res){
    var path = url.parse(req.url).pathname;

    path = '005_returnPage/' + path.substr(1);
    
fs.readFile(path, function(err, data){
    if(err){
        console.log(err);
        res.writeHead(404, { 'Content-Type' : 'text/plain'});
        res.end('Not Found!');
    }
    else{
        res.writeHead(200, { 'Content-Type' : 'text/html'});
        res.write(data.toString());
        console.log('data was sent');
        res.end();
    }
});

}).listen(8080, function(){
    console.log('Server starting!');
});