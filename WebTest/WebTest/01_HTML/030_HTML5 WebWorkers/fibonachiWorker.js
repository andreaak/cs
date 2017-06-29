function calcFib(x) {
    if (x > 1) {
        return calcFib(x - 1) + calcFib(x - 2);
    }
    else {
        return x;
    }
}

addEventListener("message", function () {

    for (var i = 0; i < 50; i++) {
        postMessage(i + " = " + calcFib(i) + "<br />");
    }

}, false);