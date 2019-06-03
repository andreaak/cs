var repl = require('repl');

var server = repl.start();

// replServer.defineCommand(keyword, cmd) Добавляет новую команду в repl
    // keyword - название комманды
    // объект со свойствами help - описание комманды, action - выполняемые действия
server.defineCommand('hello', {
    action: function(name){
        console.log('Hello', name);
    }
});