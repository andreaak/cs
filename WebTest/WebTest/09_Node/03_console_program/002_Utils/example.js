var utils = require('util');

function oldFunction(){
    console.log('This is a old function!');
}

function newFunction(){
    console.log('This is a new function!');
}

exports.oF = utils.deprecate(oldFunction, "Function is oldest. Use a newFuinction!");
exports.nF = newFunction;