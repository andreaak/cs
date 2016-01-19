var module = angular.module("customServices", []);
// factory - метод для создания сервисов. В качестве параметров принимает имя сервиса и фабричную функцию для его создания.
// Эземпляр, создаваемый фабричной функцией, будет одним на все приложение (singleton)
module.factory("logService", function () {
    var messageCount = 0;
    return {
        log: function (msg) {
            console.log("LOG #" + messageCount++ + ", message = " + msg);
        }
    };
});