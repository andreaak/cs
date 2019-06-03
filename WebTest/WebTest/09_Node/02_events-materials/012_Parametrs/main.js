var evt = require('events');

var emitter = new evt.EventEmitter();

emitter.on('click', function(a, b){
    console.log('function 1');  
    console.log(a, b);
});

emitter.on('click', function(){
    console.log('function 2'); 
    console.log(arguments);    
});

emitter.emit('click', 1, 2);