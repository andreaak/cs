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
var FormControlValComponent = (function () {
    function FormControlValComponent() {
    }
    FormControlValComponent.prototype.ngOnInit = function () {
        // Validators - класс, со статическими методами для валидции.
        // При вызове конструктора FormControl первый параметр, значение поля ввода
        // второй параметр - валидатор или массив валидаторов.
        this.loginForm = new forms_1.FormGroup({
            login: new forms_1.FormControl("user1", forms_1.Validators.required),
            password: new forms_1.FormControl("", [forms_1.Validators.required, forms_1.Validators.minLength(7)])
        });
    };
    FormControlValComponent.prototype.onSubmit = function (form) {
        console.log(form.valid);
        console.log(form.value);
    };
    FormControlValComponent = __decorate([
        core_1.Component({
            moduleId: module.id,
            selector: "form-control-sample",
            templateUrl: "form-control-val.component.html",
            styleUrls: [
                "../../../node_modules/bootstrap/dist/css/bootstrap.css"
            ]
        }), 
        __metadata('design:paramtypes', [])
    ], FormControlValComponent);
    return FormControlValComponent;
}());
exports.FormControlValComponent = FormControlValComponent;
//# sourceMappingURL=form-control-val.component.js.map