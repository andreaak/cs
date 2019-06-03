var fs = require('fs');

fs.readdir('testdir', function(err, filenames){
    console.log(filenames);
});



