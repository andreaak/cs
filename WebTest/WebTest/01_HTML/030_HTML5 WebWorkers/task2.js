
addEventListener("message", function (e) {

    var data = e.data;

    switch (data.cmd) {
        case 'start':
            postMessage("ПОТОК ЗАПУЩЕН : " + data.msg);
            break;
        case 'stop':
            postMessage("ПОТОК ОСТАНОВЛЕН : " + data.msg);
            close();
            break;
        default:
            postMessage("НЕИВЕСТНАЯ КОМАНДА : " + data.msg);
    }
}, true);