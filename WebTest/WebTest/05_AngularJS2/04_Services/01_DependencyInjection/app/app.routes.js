"use strict";
var index_1 = require("./sample02-simpleService/index");
var index_2 = require("./sample03-serviceHierarchy/index");
var index_3 = require("./sample04-providers/index");
exports.routes = [
    {
        path: "",
        redirectTo: "sample1",
        pathMatch: "full"
    },
    {
        path: "sample1",
        component: index_1.DataListComponent
    },
    {
        path: "sample2",
        component: index_2.CounterParentComponent
    },
    {
        path: "sample3",
        component: index_3.Sample1Component
    },
    {
        path: "sample4",
        component: index_3.Sample2Component
    },
    {
        path: "sample5",
        component: index_3.Sample3Component
    },
    {
        path: "sample6",
        component: index_3.Sample4Component
    },
    {
        path: "sample7",
        component: index_3.Sample5Component
    },
    {
        path: "sample8",
        component: index_3.Sample6Component
    },
    {
        path: "sample9",
        component: index_3.Sample7Component
    },
    {
        path: "sample10",
        component: index_3.Sample8Component
    }
];
//# sourceMappingURL=app.routes.js.map