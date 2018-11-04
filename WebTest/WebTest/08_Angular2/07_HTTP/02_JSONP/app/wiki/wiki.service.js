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
require('rxjs/add/operator/map');
var WikiService = (function () {
    // Jsonp сервис для работы с JSONP
    function WikiService(jsonp) {
        this.jsonp = jsonp;
    }
    WikiService.prototype.search = function (term) {
        var wikiUrl = "http://ru.wikipedia.org/w/api.php";
        var params = new http_1.URLSearchParams();
        params.set("search", term);
        params.set("action", "opensearch");
        params.set("format", "json");
        params.set("callback", "JSONP_CALLBACK");
        return this.jsonp
            .get(wikiUrl, { search: params })
            .map(function (response) {
            var responseData = response.json();
            var names = responseData[1];
            var descriptions = responseData[2];
            var links = responseData[3];
            var length = names.length;
            var result = [];
            for (var i = 0; i < length; ++i) {
                result.push({
                    name: names[i],
                    link: links[i],
                    description: descriptions[i]
                });
            }
            return result;
        });
    };
    WikiService = __decorate([
        core_1.Injectable(), 
        __metadata('design:paramtypes', [http_1.Jsonp])
    ], WikiService);
    return WikiService;
}());
exports.WikiService = WikiService;
//# sourceMappingURL=wiki.service.js.map