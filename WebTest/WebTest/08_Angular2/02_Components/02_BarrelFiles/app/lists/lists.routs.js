"use strict";
var index_1 = require("./index");
exports.routs = [
    { path: "list1", component: index_1.List1Component },
    { path: "list2", component: index_1.List2Component },
    { path: "", redirectTo: "list1", pathMatch: "full" }
];
//# sourceMappingURL=lists.routs.js.map