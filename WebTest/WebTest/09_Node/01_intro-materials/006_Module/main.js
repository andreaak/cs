var mod = require('./mod.js');
// Объект module хранит информацию о текущем модуле
console.log(module);

//Module {
//  id: '.',                                        - путь к запускаемому файлу
//  exports: {},                                    - обьект который возвращается функцией require
//  parent: null,                                   - родительский модуль
//  filename: 'D:\\Node\\006_Module\\main.js',      - Абсолютный путь к файлу
//  loaded: false,                                  - статус обработки модуля
//  children:                                       - дочерние модули
//   [ Module {
//       id: 'D:\\Node\\006_Module\\mod.js',
//       exports: {},
//       parent: [Circular],
//       filename: 'D:\\Node\\006_Module\\mod.js',
//       loaded: true,
//       children: [],
//       paths: [Object] } ],
//  paths:                                          - пути по которым происходит поиск модуля
//   [ 'D:\\Node\\006_Module\\node_modules',
//     'D:\\Node\\node_modules',
//     'D:\\node_modules' ] }
