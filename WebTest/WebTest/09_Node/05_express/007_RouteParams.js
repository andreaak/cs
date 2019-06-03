var express = require('express');
var app = express();

// В маршрутах допустимо задавать параметры, для этого необходимо в маршруте определить сегмент, в котором будет находится параметр
// для передачи параметра в сегменте, необходимо перед именем параметра установить двоеточие (:)
// http://example.com/home/index.html?category=notebooks&product=100
// http://example.com/home/index/notebooks/100
app.get('/category/:categoryId', function(request, response){
    // для получения параметра из сегмента используется сойство params объекта request
    console.log(request.params);
});

app.get('/category/:categoryId/product/:productId', function(request, response){
    console.log(`category: ${request.params['categoryId']}`);
    console.log(`product: ${request.params['productId']}`);
});

app.listen(8080);