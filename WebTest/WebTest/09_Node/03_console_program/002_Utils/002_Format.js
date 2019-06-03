var utils = require('util');

// %s - Строка
// %d - Число (целое или число с плавающей запятой) 
// %j - JSON
// %% - символ '%'
var user = {
    name : "Ivan",
    age : 25,
    salary : 10000,
    bonus: 15
}

var str = utils.format('Hello, my name is %s. I\'m %d years old! My bonuses from the salary are %d%%', user.name, user.age, user.bonus);

console.log(str);

console.log();

console.log('%j', user);