"use strict";
var product_list_component_1 = require("./product-list/product-list.component");
var product_create_edit_component_1 = require("./product-create-edit/product-create-edit.component");
var product_delete_component_1 = require("./product-delete/product-delete.component");
exports.routes = [
    {
        path: "",
        redirectTo: "products",
        pathMatch: "full"
    },
    { path: "products", component: product_list_component_1.ProductListComponent },
    { path: "products/edit/:id", component: product_create_edit_component_1.ProductCreateEditComponent },
    { path: "products/create", component: product_create_edit_component_1.ProductCreateEditComponent },
    { path: "products/delete/:id", component: product_delete_component_1.ProductDeleteComponent }
];
//# sourceMappingURL=app.routes.js.map