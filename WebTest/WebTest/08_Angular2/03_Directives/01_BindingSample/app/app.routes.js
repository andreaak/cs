"use strict";
var index_1 = require("./sample01-binding/index");
var index_2 = require("./sample02-two-way-databinding/index");
var index_3 = require("./sample03-built-in-directives/index");
var index_4 = require("./sample04-templates/index");
exports.routes = [
    {
        path: "",
        redirectTo: "sample-01",
        pathMatch: "full"
    },
    {
        path: "sample-01",
        component: index_1.InterpolationComponent
    },
    {
        path: "sample-02",
        component: index_1.PropertyBindingComponent
    },
    {
        path: "sample-03",
        component: index_1.EventBindingComponent
    },
    {
        path: "sample-04",
        component: index_1.AttributeBindingComponent
    },
    {
        path: "sample-05",
        component: index_1.ClassBindingComponent
    },
    {
        path: "sample-06",
        component: index_1.StyleBindingComponent
    },
    {
        path: "sample-07",
        component: index_2.CalcComponent
    },
    {
        path: "sample-08",
        component: index_2.SampleNgModelComponent
    },
    {
        path: "sample-09",
        component: index_3.NgClassComponent
    },
    {
        path: "sample-10",
        component: index_3.NgStyleComponent
    },
    {
        path: "sample-11",
        component: index_3.NgIfComponent
    },
    {
        path: "sample-12",
        component: index_3.NgSwitchComponent
    },
    {
        path: "sample-13",
        component: index_3.NgForComponent
    },
    {
        path: "sample-14",
        component: index_4.NgIfTemplateComponent
    },
    {
        path: "sample-15",
        component: index_4.NgSwitchTemplateComponent
    },
    {
        path: "sample-16",
        component: index_4.NgForTemplateComponent
    },
    {
        path: "sample-17",
        component: index_4.TempRefVarComponent
    }
];
//# sourceMappingURL=app.routes.js.map