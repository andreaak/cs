// платформа для браузера с компилятором.
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';
// модуль приложения.
import { AppModule } from './app.module';

// компиляция и запуск модуля.
const platform = platformBrowserDynamic();
platform.bootstrapModule(AppModule);
