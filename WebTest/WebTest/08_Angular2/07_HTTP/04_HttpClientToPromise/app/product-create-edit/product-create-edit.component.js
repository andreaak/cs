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
var forms_1 = require("@angular/forms");
var index_1 = require("../shared/index");
var ProductCreateEditComponent = (function () {
    function ProductCreateEditComponent(service, activatedRoute, fb, router) {
        this.service = service;
        this.activatedRoute = activatedRoute;
        this.fb = fb;
        this.router = router;
    }
    ProductCreateEditComponent.prototype.ngOnInit = function () {
        this.buildForm();
        this.getProductFromRoute();
    };
    ProductCreateEditComponent.prototype.checkError = function (element, errorType) {
        return this.productForm.get(element).hasError(errorType) &&
            this.productForm.get(element).touched;
    };
    ProductCreateEditComponent.prototype.onSubmit = function (productForm) {
        var _this = this;
        this.currentProduct.name = productForm.value.name;
        this.currentProduct.price = productForm.value.price;
        if (this.currentProduct.id) {
            this.service.updateProduct(this.currentProduct)
                .then(function () { return _this.goBack(); }, function (error) { return _this.errorMessage = error; });
        }
        else {
            this.service.addProduct(this.currentProduct)
                .then(function () { return _this.goBack(); }, function (error) { return _this.errorMessage = error; });
        }
    };
    ProductCreateEditComponent.prototype.goBack = function () {
        this.router.navigate(["/products"]);
    };
    ProductCreateEditComponent.prototype.getProductFromRoute = function () {
        var _this = this;
        this.activatedRoute.params.forEach(function (params) {
            var id = params["id"];
            if (id) {
                _this.service.getProduct(id).then(function (product) {
                    _this.currentProduct = product;
                    _this.productForm.patchValue(_this.currentProduct);
                }, function (error) { return _this.errorMessage = error; });
            }
            else {
                _this.currentProduct = new index_1.Product(null, null, null);
                _this.productForm.patchValue(_this.currentProduct);
            }
        });
    };
    ProductCreateEditComponent.prototype.buildForm = function () {
        this.productForm = this.fb.group({
            name: ["", forms_1.Validators.required],
            price: ["", forms_1.Validators.required]
        });
    };
    ProductCreateEditComponent = __decorate([
        core_1.Component({
            moduleId: module.id,
            selector: "product-create-edit",
            templateUrl: "product-create-edit.component.html",
            styleUrls: ["../../../node_modules/bootstrap/dist/css/bootstrap.css"]
        }), 
        __metadata('design:paramtypes', [index_1.ProductService, router_1.ActivatedRoute, forms_1.FormBuilder, router_1.Router])
    ], ProductCreateEditComponent);
    return ProductCreateEditComponent;
}());
exports.ProductCreateEditComponent = ProductCreateEditComponent;
//# sourceMappingURL=product-create-edit.component.js.map