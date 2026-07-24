import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import {
  provideHttpClient,
  withInterceptorsFromDi,
} from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NavbarComponent } from './components/shared/navbar/navbar.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ToastrModule } from 'ngx-toastr';
import { MaterialModule } from './modules/material/material.module';
import { AuthModule } from './modules/auth/auth.module';
import { playerFactory } from './config/lottie.config';
import { provideLottieOptions } from 'ngx-lottie';

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
  ],
  exports: [],
  bootstrap: [AppComponent],
  imports: [
    AuthModule,
    BrowserModule,
    MaterialModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    FormsModule,
    ReactiveFormsModule,
    ToastrModule.forRoot({}),
  ],
  providers: [provideHttpClient(withInterceptorsFromDi()), provideLottieOptions({ player: playerFactory })],
})
export class AppModule {}
