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
var http_1 = require("@angular/http");
var product_1 = require("./product");
var Observable_1 = require("rxjs/Observable");
var ProductService = (function () {
    function ProductService(http) {
        this.http = http;
        // адрес сервиса
        this.url = "http://localhost:2403/products";
    }
    // Отправка GET запроса нв сервер
    ProductService.prototype.getProducts = function () {
        var products = this.http.get(this.url)
            .map(this.extractProducts) // преобразовывает ответ в массив экземпляров Product.
            .catch(this.handleError);
        return products;
    };
    ProductService.prototype.getProduct = function (id) {
        var product = this.http.get(this.url + "/" + id)
            .map(this.extractProduct) // преобразовывает ответ в экземпляр Product.
            .catch(this.handleError);
        return product;
    };
    // Отправка POST запроса на сервер, добавление нового продукта в базу.
    ProductService.prototype.addProduct = function (product) {
        return this.http.post(this.url, product)
            .catch(this.handleError);
    };
    // Отправка PUT запроса и обновление продукта в базе.
    ProductService.prototype.updateProduct = function (product) {
        return this.http.put(this.url + "/" + product.id, product)
            .catch(this.handleError);
    };
    // Отправка DELETE запроса и удаление продукта из базы.
    ProductService.prototype.deleteProduct = function (product) {
        return this.http.delete(this.url + "/" + product.id)
            .catch(this.handleError);
    };
    ProductService.prototype.extractProducts = function (response) {
        var res = response.json();
        var products = [];
        for (var i = 0; i < res.length; i++) {
            products.push(new product_1.Product(res[i].id, res[i].name, res[i].price));
        }
        return products;
    };
    ProductService.prototype.extractProduct = function (response) {
        var res = response.json();
        var product = new product_1.Product(res.id, res.name, res.price);
        return product;
    };
    ProductService.prototype.handleError = function (error, cought) {
        var message = "";
        if (error instanceof http_1.Response) {
            var errorData = error.json().error || JSON.stringify(error.json());
            message = error.status + " - " + (error.statusText || '') + " " + errorData;
        }
        else {
            message = error.message ? error.message : error.toString();
        }
        console.error(message);
        return Observable_1.Observable.throw(message);
    };
    ProductService = __decorate([
        core_1.Injectable(), 
        __metadata('design:paramtypes', [http_1.Http])
    ], ProductService);
    return ProductService;
}());
exports.ProductService = ProductService;
//# sourceMappingURL=product.service.js.map