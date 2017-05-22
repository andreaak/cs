"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
var core_1 = require("@angular/core");
var platform_browser_1 = require("@angular/platform-browser");
var router_1 = require("@angular/router");
var http_1 = require("@angular/http");
var forms_1 = require("@angular/forms");
var app_component_1 = require("./app.component");
var app_routes_1 = require("./app.routes");
var product_list_component_1 = require("./product-list/product-list.component");
var product_create_edit_component_1 = require("./product-create-edit/product-create-edit.component");
var product_delete_component_1 = require("./product-delete/product-delete.component");
var index_1 = require("./shared/index");
var AppModule = (function () {
    function AppModule() {
    }
    AppModule = __decorate([
        core_1.NgModule({
            imports: [
                platform_browser_1.BrowserModule,
                http_1.HttpModule,
                forms_1.ReactiveFormsModule,
                router_1.RouterModule.forRoot(app_routes_1.routes)
            ],
            declarations: [
                app_component_1.AppComponent,
                product_list_component_1.ProductListComponent,
                product_create_edit_component_1.ProductCreateEditComponent,
                product_delete_component_1.ProductDeleteComponent],
            bootstrap: [app_component_1.AppComponent],
            providers: [index_1.ProductService]
        }), 
        __metadata('design:paramtypes', [])
    ], AppModule);
    return AppModule;
}());
exports.AppModule = AppModule;
//# sourceMappingURL=app.module.js.map