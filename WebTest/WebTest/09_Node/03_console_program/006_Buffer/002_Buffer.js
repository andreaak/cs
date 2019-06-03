console.log(Buffer.byteLength('hello world', 'utf-8'));

var buff1 = Buffer.from([10, 20, 30, 40, 50, 60, 70, 80, 90]);
var buff2 = Buffer.from('Hello world!!!', 'utf8');
// Возможные значения кодировок
    // 'ascii' - только для 7-битных ASCII-cтpoк. Этот метод кодирования очень быстрый, он сбрасывает старший бит символа;
    // 'utf' - Uniсоdе-символы UTF-8;
    // 'binary' - хранит двоичные данные в строке, используя младшие 8 бит каждого символа.
    // 'base64' - строка, закодированная в системе Base64;
    // 'hex' - кодирует каждый байт как два шестнадцатеричных символа
var buff3 = Buffer.from([50, 60, 70, 80, 90, 10, 20, 30, 40]);

console.log(buff1[2]);

// buf.compare(target[, targetStart[, targetEnd[, sourceStart[, sourceEnd]]]])
// метод сравнивает два буфера, и возвращает 0 - если буферы равны, 1 если buf > target, -1 если buf < target
console.log('Compare buffer:', buff1.compare(buff2)); 
console.log('index compare:', buff1.compare(buff3, 5, 9, 0, 4));

// Buffer.concat метод объединяющий несколько буферов в один
var newBuff = Buffer.concat([buff1, buff2]);
console.log(newBuff);
// метод проверяет является ли параметр типом Buffer
console.log(Buffer.isBuffer(newBuff));

var symb = buff2.indexOf('l');
console.log('Position of symbol \'l\' from start', symb);

symb = buff2.lastIndexOf('l');
console.log('Position of symbol \'l\' from end', symb);



