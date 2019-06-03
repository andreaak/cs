var arr = [];
// функция console.time(label) позволяет запустить таймер, для замера продолжительности выполнения операции.
console.time('1-st');

for (var index = 0; index < 1000000; index++) {
    arr[index] = (index + 1) * 2;    
}
// функция console.timeEnd(label) останавливает таймер который был вызван функцией console.time(label)
console.timeEnd('1-st');    // Время в миллисекундах