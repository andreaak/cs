var http = require('http');
var url = require('url');

http.createServer(function (req, res) {

    var query = url.parse(req.url, true).query;
    res.end('GET params: ' + JSON.stringify(query));

}).listen(8080, function () {
    console.log('Server running on port 8080'); 
});