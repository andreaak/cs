var module = angular.module("customServices", []);

var baseLogger = function () {
    this.messageCount = 0;
    this.log = function (msg) {
        console.log("Type " + this.msgType + " LOG #" + this.messageCount++ + ", message = " + msg);
    }
}

var debugLogger = function () { }
debugLogger.prototype = new baseLogger();
debugLogger.prototype.msgType = "Debug";

var errorLogger = function () { }
errorLogger.prototype = new baseLogger();
errorLogger.prototype.msgType = "Error";

// service - метод для создания сервисов. При использовании данного метода фабричная функция работает как конструктор.
// Для создания сервисов AngularJS будет запускать эту функцию с использованием ключевого слова new
module.service("logService", debugLogger)
      .service("errorService", errorLogger);