var App = App || {};

App.define = function (namespace) {
    var parts = namespace.split("."),
        parent = App,
        i;

    // убрать начальный префикс если это имя глобальной переменной
    if (parts[0] === "App") {
        parts = parts.slice(1); // ['utils', 'dom']
    }

    // если в глобальном объекте нет свойства - создать его.
    for (i = 0; i < parts.length; i++) {

        if (typeof parent[parts[i]] == "undefined") {
            parent[parts[i]] = {};
        }

        parent = parent[parts[i]];
    }
    return parent;
}