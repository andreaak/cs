var fs = require('fs');

fs.watch('text.txt', function(event, filename){
    console.log('File %s is %s.', filename, event);
});

fs.writeFile('text.txt', 'test string', function(err){
    if(err) throw err;
    console.log('data was wrote');
});