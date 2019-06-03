var http = require('http');
// Создание POST запроса 
// данные для передачи с POST запросом
var postData = 'This is sample POST data!'; 

// Параметры создаваемого запроса 
var config = {
    host: 'localhost',
    port: 8080,
    method: 'POST',
    headers: { 'Content-Type': 'text/plain' }

}; 


// сделать запрос на сервер 
var req = http.request(config, (res) => {

    console.log(`STATUS: ${res.statusCode}`);
    console.log(`HEADERS: ${JSON.stringify(res.headers)}`);

    res.setEncoding('utf8');

    res.on('data', (chunk) => {
        console.log(`BODY: ${chunk}`);
    });

    res.on('end', () => {
        console.log('No more data in response.');
    });

});

req.write(postData); // запись данных в тело запроса 
req.end(); 

console.log('server running on port 8080'); 
