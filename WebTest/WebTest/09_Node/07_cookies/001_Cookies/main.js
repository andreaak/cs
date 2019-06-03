var express = require('express');
var app = express();

app.use(function(req, res){
    console.log(req.headers['cookie']);
    // Отправляем файл
    res.sendFile(__dirname + '/index.html');

});
app.listen(8080, function(){
    console.log('Server started');
});