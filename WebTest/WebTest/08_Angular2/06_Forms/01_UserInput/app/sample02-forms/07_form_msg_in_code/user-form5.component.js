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
var forms_1 = require("@angular/forms");
var user_1 = require("../user");
var UserForm5Component = (function () {
    function UserForm5Component() {
        this.roles = ["Guest", "Moderator", "Administartor"];
        this.model = new user_1.User(1, "", "", null);
        this.submitted = false;
        // Объект с ошибками, которые будут выведены в пользовательском интерфейсе
        this.formErrors = {
            "name": "",
            "age": ""
        };
        // Объект с сообщениями ошибок
        this.validationMessages = {
            "name": {
                "required": "Обязательное поле.",
                "minlength": "Значение должно быть не менее 4х символов.",
            },
            "age": {
                "required": "Обязательное поле."
            }
        };
    }
    UserForm5Component.prototype.ngAfterViewInit = function () {
        var _this = this;
        this.userForm.valueChanges.subscribe(function (data) { return _this.onValueChanged(data); });
    };
    UserForm5Component.prototype.onValueChanged = function (data) {
        if (!this.userForm)
            return;
        var form = this.userForm.form;
        for (var field in this.formErrors) {
            this.formErrors[field] = "";
            // form.get - получение элемента управления
            var control = form.get(field);
            if (control && control.dirty && !control.valid) {
                var message = this.validationMessages[field];
                for (var key in control.errors) {
                    this.formErrors[field] += message[key] + " ";
                }
            }
        }
    };
    UserForm5Component.prototype.onSubmit = function () {
        this.submitted = true;
        console.log("submitted");
    };
    __decorate([
        core_1.ViewChild('userForm'), 
        __metadata('design:type', forms_1.NgForm)
    ], UserForm5Component.prototype, "userForm", void 0);
    UserForm5Component = __decorate([
        core_1.Component({
            moduleId: module.id,
            selector: "user-form5",
            templateUrl: "user-form5.component.html",
            styleUrls: [
                "../../../node_modules/bootstrap/dist/css/bootstrap.css",
                "user-form5.component.css"
            ]
        }), 
        __metadata('design:paramtypes', [])
    ], UserForm5Component);
    return UserForm5Component;
}());
exports.UserForm5Component = UserForm5Component;
//# sourceMappingURL=user-form5.component.js.map