var fs = require('fs');

fs.exists('text.txt', function(){
    fs.readFile('text.txt', {encoding : 'utf-8'}, function(err, data){
        if(err){
            console.log(err);
            return;
        }
        var obj = JSON.parse(data);
        console.log(obj.fname, obj.lname, obj.age, obj.position);
    });
});

