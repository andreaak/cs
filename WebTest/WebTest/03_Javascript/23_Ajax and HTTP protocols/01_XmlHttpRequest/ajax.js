function createXHR() {
    if (typeof XMLHttpRequest != "undefined") {
        return new XМLHttpRequest();
    } else if (typeof ActiveXObject != "undefined") {
        if (typeof arguments.callee.activeXString != "string") {

            var versions = ["MSXML2.XMLHttp.6.0", "MSXML2.XMLHttp.3.0", "MSXML2.XMLHttp"],
                i,
                len;
            for (i = 0, len = versions.length; i < len; i++) {

                try {
                    new ActiveXObject(versions[i]);
                    arguments.callee.activeXString = versions[i];
                    break;
                } catch (ех) {
                    //никакой код не требуется
                }
            }
        }
        return new ActiveXObject(arguments.callee.activeXString);
    } else {
        throw new Error("No XHR object available.");
    }
}

//функция для IE до версии 7
function createXHRIE() {
    if (typeof arguments.callee.activeXString != "string") {

        var versions = ["MSXML2.XMLHttp.6.0", "MSXML2.XMLHttp.3.0", "MSXML2.XMLHttp"],
            i,
            len;
        for (i = 0, len = versions.length; i < len; i++) {

            try {
                new ActiveXObject(versions[i]);
                arguments.callee.activeXString = versions[i];
                break;
            } catch (ех) {
                //никакой код не требуется
            }
        }
    }
    return new ActiveXObject(arguments.callee.activeXString);
}
