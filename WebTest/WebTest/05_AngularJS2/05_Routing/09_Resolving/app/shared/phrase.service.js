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
var phrase_1 = require("./phrase");
var phrases = [
    new phrase_1.Phrase(1, "Hello World", "English"),
    new phrase_1.Phrase(2, "Hola Mundo", "Spanish"),
    new phrase_1.Phrase(3, "Привіт Світ", "Ukrainian"),
    new phrase_1.Phrase(4, "Bonjour le monde", "French"),
    new phrase_1.Phrase(5, "Hallo Welt", "German"),
    new phrase_1.Phrase(6, "Привет Мир", "Russian"),
    new phrase_1.Phrase(7, "Ciao mondo", "Italian"),
    new phrase_1.Phrase(8, "Witaj świecie", "Polish"),
    new phrase_1.Phrase(9, "Hej världen", "Swdish"),
    new phrase_1.Phrase(10, "Pozdravljen, svet", "Slovenian"),
    new phrase_1.Phrase(11, "Прывітанне Сусвет", "Belarusian")
];
// Promise, который сразу переходит в состояние resolved с данными из массива phrases
var phrasesPromise = Promise.resolve(phrases);
// Сервис для работы с данными.
// в будущем его можно переделать на работу с сервером
var PhraseService = (function () {
    function PhraseService() {
    }
    // Метод для получения всех фраз. Возвращает Promise с массивом Phrase
    PhraseService.prototype.getAll = function () {
        return phrasesPromise;
    };
    // Метод для получения фразы по id. Возвращает Promise c экземпляром Phrase
    PhraseService.prototype.getPhrase = function (id) {
        return phrasesPromise
            .then(function (phrases) { return phrases.find(function (x) { return x.id == id; }); });
    };
    PhraseService = __decorate([
        core_1.Injectable(), 
        __metadata('design:paramtypes', [])
    ], PhraseService);
    return PhraseService;
}());
exports.PhraseService = PhraseService;
//# sourceMappingURL=phrase.service.js.map