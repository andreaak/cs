"use strict";
var index_1 = require("./sample01-input/index");
var index_2 = require("./sample02-forms/index");
var index_3 = require("./sample03-reactiveForms/index");
exports.routes = [
    {
        path: "",
        redirectTo: "sample1",
        pathMatch: "full"
    },
    {
        path: "sample1",
        component: index_1.EventSampleComponent
    },
    {
        path: "sample2",
        component: index_1.EventObjectSampleComponent
    },
    {
        path: "sample3",
        component: index_1.TemplateRefSampleComponent
    },
    {
        path: "sample4",
        component: index_1.KeyEnterSampleComponent
    },
    {
        path: "sample5",
        component: index_1.BlurSampleComponent
    },
    {
        path: "sample6",
        component: index_1.MyListComponent
    },
    {
        path: "sample7",
        component: index_2.UserFormComponent
    },
    {
        path: "sample8",
        component: index_2.StylesSampleComponent
    },
    {
        path: "sample9",
        component: index_2.UserForm2Component
    },
    {
        path: "sample10",
        component: index_2.UserForm3Component
    },
    {
        path: "sample11",
        component: index_2.FormErrorsComponent
    },
    {
        path: "sample12",
        component: index_2.UserForm4Component
    },
    {
        path: "sample13",
        component: index_2.UserForm5Component
    },
    {
        path: "sample14",
        component: index_3.FormControlComponent
    },
    {
        path: "sample15",
        component: index_3.FormControlValComponent
    },
    {
        path: "sample16",
        component: index_3.FormBuilderComponent
    },
    {
        path: "sample17",
        component: index_3.ReactiveFormComponent
    },
    {
        path: "sample18",
        component: index_3.CustomValidatorsComponent
    }
];
//# sourceMappingURL=app.routes.js.map