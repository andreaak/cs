"use strict";
// платформа для браузера с компилятором.
var platform_browser_dynamic_1 = require('@angular/platform-browser-dynamic');
// модуль приложения.
var app_module_1 = require('./app.module');
// компиляция и запуск модуля.
var platform = platform_browser_dynamic_1.platformBrowserDynamic();
platform.bootstrapModule(app_module_1.AppModule);
//# sourceMappingURL=main.js.map