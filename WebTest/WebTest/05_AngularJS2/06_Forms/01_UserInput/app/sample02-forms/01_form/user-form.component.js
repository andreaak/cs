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
var user_1 = require("../user");
var UserFormComponent = (function () {
    function UserFormComponent() {
        this.roles = ["Guest", "Moderator", "Administartor"];
        this.model = new user_1.User(0, "", "", 0);
        this.submitted = false;
    }
    UserFormComponent.prototype.onSubmit = function () {
        this.submitted = true;
    };
    Object.defineProperty(UserFormComponent.prototype, "diagnostic", {
        get: function () { return JSON.stringify(this.model); },
        enumerable: true,
        configurable: true
    });
    UserFormComponent = __decorate([
        core_1.Component({
            moduleId: module.id,
            selector: "user-form",
            templateUrl: "user-form.component.html",
            styleUrls: ["../../../node_modules/bootstrap/dist/css/bootstrap.css"]
        }), 
        __metadata('design:paramtypes', [])
    ], UserFormComponent);
    return UserFormComponent;
}());
exports.UserFormComponent = UserFormComponent;
//# sourceMappingURL=user-form.component.js.map