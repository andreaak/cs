var evt = require('events');

// Создаем функцию конструктор, которая будет унаследована от функции EventEmitter
// Объект созданый функцией позволит подключиться к файлу и считать из него данные

function ReadFile(){
    this._file = "";
}
// В прототип ридера записываем объект EventEmitter что бы была возможность генерировать события
ReadFile.prototype = new evt.EventEmitter();

// fileName - имя файла
// callback - функция, которая  вызовется после того как данные будут прочитаны
ReadFile.prototype.readDataFromFile = function(path, callback){
    this.file = path;
    if(typeof callback == 'function'){
        this.on('readData', callback)
    }
    this._read();
};

ReadFile.prototype._read = function(){
    console.log('Loading...');
    var someDataFromFile = 'Text text text';    // данные считанные из файла
    this.emit('readData', someDataFromFile);    
    console.log('Data was read.');
}

module.exports.Reader = ReadFile;




