"use strict";
var index_1 = require("./sample01_inMemServer/index");
exports.routes = [
    {
        path: "",
        redirectTo: "sample1",
        pathMatch: "full"
    },
    {
        path: "sample1",
        component: index_1.GetRequestComponent
    },
    {
        path: "sample2",
        component: index_1.PostDataComponent
    },
    {
        path: "sample3",
        component: index_1.RequestOptionsComponent
    },
    {
        path: "sample4",
        component: index_1.SearchParamsComponent
    },
];
//# sourceMappingURL=app.routes.js.map