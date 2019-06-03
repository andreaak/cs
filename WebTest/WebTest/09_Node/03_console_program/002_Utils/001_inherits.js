// 
var utils = require('util');

function Base(){
    this.name = "Base function"
}
Base.prototype.say = function(){
    console.log('Hello, this is a %s function', this.name);
}

function Derived(){
    this.name = "Derived";
}
// Метод utils.inherits позволяет производить наследование
utils.inherits(Derived, Base);

Derived.prototype.getData = function(){
    console.log('Some data from Derived function')
}

var derived = new Derived();

derived.getData();
derived.say();

//exports.inherits = function(ctor, superCtor) {
// 
//  if (ctor === undefined || ctor === null)
//    throw new TypeError('The constructor to `inherits` must not be ' +
//                        'null or undefined.');
// 
//  if (superCtor === undefined || superCtor === null)
//    throw new TypeError('The super constructor to `inherits` must not ' +
//                        'be null or undefined.');
// 
//  if (superCtor.prototype === undefined)
//    throw new TypeError('The super constructor to `inherits` must ' +
//                        'have a prototype.');
// 
//  ctor.super_ = superCtor;
//  ctor.prototype = Object.create(superCtor.prototype, {
//    constructor: {
//      value: ctor,
//      enumerable: false,
//      writable: true,
//      configurable: true
//    }
//  });
//};