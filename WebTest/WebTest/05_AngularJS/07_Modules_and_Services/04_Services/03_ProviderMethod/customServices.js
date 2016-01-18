var module = angular.module("customServices", []);
// provider - метод для создания сервисов. В качестве аргумента принимает имя сервиса и фабричную функцию.
// Фабричная функция должна вернуть Provider Object, который содержит метод $get.
// Метод $get должен возвращать сервис.
// Подход с использование provider функции позволяет выполнять дополнительную конфигурацию сервисов (см. 001_index.html)
module.provider("logService", function () {

    var counter = true;
    var debug = true;

    return {
        messageCounterEnabled: function (setting) {
            if (angular.isDefined(setting)) {
                counter = setting;
                return this;
            } else {
                return counter;
            }
        },
        debugEnabled: function (setting) {
            if (angular.isDefined(setting)) {
                debug = setting;
                return this;
            } else {
                return debug;
            }
        },
        $get: function () {
            return {
                messageCount: 0,
                log: function (msg) {
                    if (debug) {
                        console.log("(LOG"
                            + (counter ? " + " + this.messageCount++ + ") " : ") ")
                            + msg);
                    }
                }
            };
        }
    }
});