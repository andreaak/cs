// path модуль ядра, обеспечивающий работу с путями директорий и файлов
var path = require('path');

var file = '004_Path_2.js';
// метод join предназначен для генерации путей, на основе принятых параметров
var filePath = path.join(__dirname, file);

console.log(filePath);
// Парсит заданый путь и возвращает объект
var pathParts = path.parse(filePath);

console.log(pathParts);

console.log('Systems separator:', path.sep);  

var objFormated = {
    root: 'D:\\',
    dir: 'D:\\Node\\003_ConosleProgram\\001_Environment',
    base: '004_Path_2.js',
    ext: '.js',
    name: '004_Path_2'    
}
// Метод format генерирует новый путь на основе свойств объекта
var obj = path.format(objFormated);

console.log(obj);   