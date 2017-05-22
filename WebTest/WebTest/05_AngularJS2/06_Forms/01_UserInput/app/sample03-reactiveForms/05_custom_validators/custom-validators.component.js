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
var custom_validators_1 = require("./custom-validators");
var CustomValidatorsComponent = (function () {
    function CustomValidatorsComponent(fb) {
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
                "emailValidator": "Не правильный формат email адреса."
            },
            "role": {
                "required": "Обязательное поле."
            },
            "age": {
                "required": "Обязательное поле.",
                "rangeValidator": "Значение должно быть в диапазоне от 10 до 100."
            }
        };
    }
    CustomValidatorsComponent.prototype.ngOnInit = function () {
        this.buildForm();
    };
    CustomValidatorsComponent.prototype.buildForm = function () {
        var _this = this;
        this.userForm = this.fb.group({
            "name": [this.user.name, [
                    forms_1.Validators.required,
                    forms_1.Validators.minLength(4),
                    forms_1.Validators.maxLength(15)
                ]],
            "email": [this.user.email, [
                    forms_1.Validators.required,
                    custom_validators_1.emailValidator // использование пользовательского валидатора
                ]],
            "role": [this.user.role, [
                    forms_1.Validators.required
                ]],
            "age": [this.user.age, [
                    forms_1.Validators.required,
                    custom_validators_1.rangeValidator(10, 100)
                ]]
        });
        this.userForm.valueChanges
            .subscribe(function (data) { return _this.onValueChange(data); });
        this.onValueChange();
    };
    CustomValidatorsComponent.prototype.onValueChange = function (data) {
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
    CustomValidatorsComponent.prototype.onSubmit = function () {
        console.log("submitted");
        console.log(this.userForm.value);
    };
    CustomValidatorsComponent = __decorate([
        core_1.Component({
            moduleId: module.id,
            selector: "custom-validators",
            templateUrl: "custom-validators.component.html",
            styleUrls: [
                "../../../node_modules/bootstrap/dist/css/bootstrap.css",
                "custom-validators.component.css"
            ]
        }), 
        __metadata('design:paramtypes', [forms_1.FormBuilder])
    ], CustomValidatorsComponent);
    return CustomValidatorsComponent;
}());
exports.CustomValidatorsComponent = CustomValidatorsComponent;
//# sourceMappingURL=custom-validators.component.js.map