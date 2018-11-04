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
var router_1 = require("@angular/router");
var index_1 = require("../shared/index");
var ProductListComponent = (function () {
    function ProductListComponent(service, router) {
        this.service = service;
        this.router = router;
    }
    ProductListComponent.prototype.ngOnInit = function () {
        this.getProducts();
    };
    ProductListComponent.prototype.refresh = function () {
        this.getProducts();
    };
    ProductListComponent.prototype.editProduct = function (product) {
        this.router.navigate(["products", "edit", product.id]);
    };
    ProductListComponent.prototype.deleteProduct = function (product) {
        this.router.navigate(["products", "delete", product.id]);
    };
    ProductListComponent.prototype.createProduct = function () {
        this.router.navigate(["products", "create"]);
    };
    ProductListComponent.prototype.getProducts = function () {
        var _this = this;
        this.service.getProducts().then(function (products) { return _this.products = products; }, function (error) { return _this.errorMessage = error; });
    };
    ProductListComponent = __decorate([
        core_1.Component({
            moduleId: module.id,
            selector: "product-list",
            templateUrl: "product-list.component.html",
            styleUrls: ["../../../node_modules/bootstrap/dist/css/bootstrap.css"]
        }), 
        __metadata('design:paramtypes', [index_1.ProductService, router_1.Router])
    ], ProductListComponent);
    return ProductListComponent;
}());
exports.ProductListComponent = ProductListComponent;
//# sourceMappingURL=product-list.component.js.map