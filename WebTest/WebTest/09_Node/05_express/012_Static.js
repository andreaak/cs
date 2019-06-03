var express = require('express');
var path = require('path');
var app = express();

var catalog = 'data/012_example';

// компонент  express.static() указывает на каталог с файлами
app.use('/', express.static(path.join(__dirname ,catalog)));

// app.get('/', function(req, res){    
//     res.sendFile(path.join(__dirname, catalog, 'index.html'));
// });

app.listen(8080);