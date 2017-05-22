"use strict";
var index_1 = require("./01_components/index");
var index_2 = require("./02_lifecycle/index");
var index_3 = require("./03_child_and_content/index");
exports.routes = [
    { path: "", redirectTo: "sample1", pathMatch: "full" },
    { path: "sample1", component: index_1.BookComponent },
    { path: "sample2", component: index_1.CounterHostComponent },
    { path: "sample3", component: index_1.TimerHostComponent },
    { path: "sample4", component: index_1.NameCardHostComponent },
    { path: "sample5", component: index_1.MessageBoxHostComponent },
    { path: "sample6", component: index_2.Sample1HostComponent },
    { path: "sample7", component: index_2.Sample2HostComponent },
    { path: "sample8", component: index_2.Sample3HostComponent },
    { path: "sample9", component: index_2.Sample4HostComponent },
    { path: "sample10", component: index_3.BlockHostComponent },
    { path: "sample11", component: index_3.Block2HostComponent },
    { path: "sample12", component: index_3.ListHostComponent },
];
//# sourceMappingURL=app.routes.js.map