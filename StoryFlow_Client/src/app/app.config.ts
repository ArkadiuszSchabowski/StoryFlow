import { ApplicationConfig, importProvidersFrom } from '@angular/core';
import { provideClientHydration, withEventReplay } from '@angular/platform-browser';
import { AppModule } from './app.module';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { errorInterceptor } from './interceptors/error.interceptor';

export const appConfig: ApplicationConfig = {
  providers: [
    importProvidersFrom(AppModule),
    provideClientHydration(withEventReplay()),
    provideHttpClient(withInterceptors([errorInterceptor])),
  ],
};