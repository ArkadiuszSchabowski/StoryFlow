import { ApplicationConfig, importProvidersFrom } from '@angular/core';
import { provideClientHydration, withEventReplay } from '@angular/platform-browser';
import { AppModule } from './app.module';

export const appConfig: ApplicationConfig = {
  providers: [
    importProvidersFrom(AppModule),
    provideClientHydration(withEventReplay()),
  ],
};