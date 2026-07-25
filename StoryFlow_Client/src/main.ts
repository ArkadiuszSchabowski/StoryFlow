import { AppModule } from './app/app.module';
import { importProvidersFrom } from '@angular/core';
import { bootstrapApplication, provideClientHydration, withEventReplay } from '@angular/platform-browser';
import { AppComponent } from './app/app.component';


  bootstrapApplication(AppComponent, {
  providers: [importProvidersFrom(AppModule), provideClientHydration(withEventReplay())],
}).catch((err) => console.error(err));
