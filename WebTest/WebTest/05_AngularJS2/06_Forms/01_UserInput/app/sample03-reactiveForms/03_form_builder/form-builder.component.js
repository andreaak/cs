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
var FormBuilderComponent = (function () {
    function FormBuilderComponent(fb) {
        this.fb = fb;
    }
    FormBuilderComponent.prototype.ngOnInit = function () {
        // FormBuilder - класс, предоставляющий удобный интерфейс для создания экземпляров FormControl и FormGroup  
        this.loginForm = this.fb.group({
            login: ["user1", forms_1.Validators.required],
            password: ["", [forms_1.Validators.required, forms_1.Validators.minLength(7)]]
        });
    };
    FormBuilderComponent.prototype.onSubmit = function (form) {
        console.log(form.valid);
        console.log(form.value);
    };
    FormBuilderComponent = __decorate([
        core_1.Component({
            moduleId: module.id,
            selector: "form-builder-sample",
            templateUrl: "form-builder.component.html",
            styleUrls: [
                "../../../node_modules/bootstrap/dist/css/bootstrap.css"
            ]
        }), 
        __metadata('design:paramtypes', [forms_1.FormBuilder])
    ], FormBuilderComponent);
    return FormBuilderComponent;
}());
exports.FormBuilderComponent = FormBuilderComponent;
//# sourceMappingURL=form-builder.component.js.map