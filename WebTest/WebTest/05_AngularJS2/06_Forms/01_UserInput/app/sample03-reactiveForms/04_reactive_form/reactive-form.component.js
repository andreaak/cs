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
var ReactiveFormComponent = (function () {
    function ReactiveFormComponent(fb) {
        this.fb = fb;
        this.user = new user_1.User();
        this.roles = ["Guest", "Moderator", "Administartor"];
        // Объект с ошибками, которые будут выведены в пользовательском интерфейсе
        this.formErrors = {
            "name": "",
            "age": "",
            "email": "",
            "role": ""
        };
        // Объект с сообщениями ошибок
        this.validationMessages = {
            "name": {
                "required": "Обязательное поле.",
                "minlength": "Значение должно быть не менее 4х символов.",
                "maxlength": "Значение не должно быть больше 15 символов."
            },
            "email": {
                "required": "Обязательное поле.",
                "pattern": "Не правильный формат email адреса."
            },
            "role": {
                "required": "Обязательное поле."
            },
            "age": {
                "required": "Обязательное поле.",
                "pattern": "Значение должно быть целым числом."
            }
        };
    }
    ReactiveFormComponent.prototype.ngOnInit = function () {
        this.buildForm();
    };
    ReactiveFormComponent.prototype.buildForm = function () {
        var _this = this;
        this.userForm = this.fb.group({
            "name": [this.user.name, [
                    forms_1.Validators.required,
                    forms_1.Validators.minLength(4),
                    forms_1.Validators.maxLength(15)
                ]],
            "email": [this.user.email, [
                    forms_1.Validators.required,
                    forms_1.Validators.pattern("[a-zA-Z0-9._%-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}")
                ]],
            "role": [this.user.role, [
                    forms_1.Validators.required
                ]],
            "age": [this.user.age, [
                    forms_1.Validators.required,
                    forms_1.Validators.pattern("\\d+")
                ]]
        });
        this.userForm.valueChanges
            .subscribe(function (data) { return _this.onValueChange(data); });
        this.onValueChange();
    };
    ReactiveFormComponent.prototype.onValueChange = function (data) {
        if (!this.userForm)
            return;
        var form = this.userForm;
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
    ReactiveFormComponent.prototype.onSubmit = function () {
        console.log("submitted");
        console.log(this.userForm.value);
    };
    ReactiveFormComponent = __decorate([
        core_1.Component({
            moduleId: module.id,
            selector: "reactive-form",
            templateUrl: "reactive-form.component.html",
            styleUrls: [
                "../../../node_modules/bootstrap/dist/css/bootstrap.css",
                "reactive-form.component.css"
            ]
        }), 
        __metadata('design:paramtypes', [forms_1.FormBuilder])
    ], ReactiveFormComponent);
    return ReactiveFormComponent;
}());
exports.ReactiveFormComponent = ReactiveFormComponent;
//# sourceMappingURL=reactive-form.component.js.map