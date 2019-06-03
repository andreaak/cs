// Класс Buffer позволяет работать с потоками двоичных данных.
// Он является глобальным объектом, поэтому не нужно использовать функцию require 

// Метод Buffer.alloc - создает буфер, проинициализированный нулями на указанное количество байт 
var buff1 = Buffer.alloc(10);
console.log('empty buffer:', buff1);

// Метод Buffer.allocUnsafe создает непроинициализированный буфер 
var nibuff = Buffer.allocUnsafe(10);
console.log('uninitialized buffer:', nibuff);

console.log('Lenght of buffer:', buff1.length);