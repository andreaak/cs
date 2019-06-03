var evt = require('events').EventEmitter;

var emt = new evt();

// emt.on('error', (err) => {
//     console.log('Processing error!');
// });

emt.emit('error'/*, new Error('test error')*/);