//На практике обьект global не используется
//Переменные обьявленные внутри модуля, являются локальными для модуля
var x = 10;

function test(){
    console.log('Test function, number = ', x);
};

// для того чтобы сделать переменные доступны в другом модуле, необходимо добавить их в обьект exports
// exports - объект, который возвращается функцией require

exports.number = x;
exports.func = test;

// exports = x;  // Error
// module.exports = x; //Normal