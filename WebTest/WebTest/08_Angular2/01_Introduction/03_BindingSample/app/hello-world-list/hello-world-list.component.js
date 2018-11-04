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
var PHRASES = [
    { value: "Hello World", language: "English" },
    { value: "Привет Мир", language: "Russian" },
    { value: "Привіт Світ", language: "Ukrainian" },
    { value: "Hola Mundo", language: "Spanish" },
    { value: "Bonjour le monde", language: "French" },
    { value: "Hallo Welt", language: "German" },
    { value: "Ciao mondo", language: "Italian" },
    { value: "Witaj świecie", language: "Polish" },
    { value: "Hej världen", language: "Swdish" },
    { value: "Pozdravljen, svet", language: "Slovenian" },
    { value: "Прывітанне Сусвет", language: "Belarusian" }
];
var HelloWorldListComponent = (function () {
    function HelloWorldListComponent() {
        this.PhraseList = PHRASES;
    }
    // Обработчик события, к которому привязаны элементы li из файла hello-world-list.component.html
    HelloWorldListComponent.prototype.onSelect = function (selected) {
        this.selectedPhraseLanguage = selected.language;
    };
    HelloWorldListComponent = __decorate([
        core_1.Component({
            selector: "hello-world-list",
            templateUrl: "app/hello-world-list/hello-world-list.component.html",
            styleUrls: ["app/hello-world-list/hello-world-list.component.css"]
        }), 
        __metadata('design:paramtypes', [])
    ], HelloWorldListComponent);
    return HelloWorldListComponent;
}());
exports.HelloWorldListComponent = HelloWorldListComponent;
//# sourceMappingURL=hello-world-list.component.js.map