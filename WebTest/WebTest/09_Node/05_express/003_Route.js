var express = require('express');
var app = express();

// в качестве маршрута можно задать регулярное выражение, которому должен соответствовать маршрут
app.get('/[a-zA-Z]*\.html$/', function(request, response){
    response.send(request.url);
    response.end();
});

app.listen(8080, function(){
    console.log('Server started on port: 8080');
})