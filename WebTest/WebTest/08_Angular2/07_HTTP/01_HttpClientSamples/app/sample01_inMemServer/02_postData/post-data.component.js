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
var PostDataComponent = (function () {
    function PostDataComponent(http) {
        this.http = http;
    }
    PostDataComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.http.get("app/items").subscribe(function (result) { return _this.itemArray = result.json().data; }, function (error) { return console.log(error.statusText); });
    };
    PostDataComponent.prototype.clickHandler = function () {
        var _this = this;
        this.http.post("app/items", {
            id: this.id,
            name: this.name
        }).subscribe(function (result) {
            var json = result.json();
            if (json)
                _this.itemArray.push(json.data);
        }, function (error) { return console.log(error.statusText); });
    };
    PostDataComponent = __decorate([
        core_1.Component({
            moduleId: module.id,
            selector: "post-data",
            templateUrl: "post-data.component.html"
        }), 
        __metadata('design:paramtypes', [http_1.Http])
    ], PostDataComponent);
    return PostDataComponent;
}());
exports.PostDataComponent = PostDataComponent;
//# sourceMappingURL=post-data.component.js.map