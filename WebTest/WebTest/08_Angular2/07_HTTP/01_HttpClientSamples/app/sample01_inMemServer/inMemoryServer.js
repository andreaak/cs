"use strict";
var ItemData = (function () {
    function ItemData() {
    }
    ItemData.prototype.createDb = function () {
        var items = [
            { id: 1, name: 'Item 1' },
            { id: 2, name: 'Item 2' },
            { id: 3, name: 'Item 3' },
            { id: 4, name: 'Item 4' }
        ];
        return { items: items };
    };
    return ItemData;
}());
exports.ItemData = ItemData;
//# sourceMappingURL=inMemoryServer.js.map