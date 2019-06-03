var utils = require('util');

var obj = { name: "Some object" };
var numb = 10;
var str = "Some string";
var und = null;
var arr = [1,2,3,4,5];

console.log('arr is array:', utils.isArray(arr));
console.log('obj is object:', utils.isObject(obj));
console.log('numb is number:', utils.isNumber(numb));
console.log('str is string:', utils.isString(str));
console.log('und is NULL:', utils.isNull(und));
console.log('und is function:', utils.isFunction(und));