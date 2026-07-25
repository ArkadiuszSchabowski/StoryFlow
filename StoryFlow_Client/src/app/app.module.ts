import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import {
  provideHttpClient,
  withInterceptorsFromDi,
} from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';
import { playerFactory } from './config/lottie.config';
import { provideLottieOptions } from 'ngx-lottie';

@NgModule({
  declarations: [
  ],
  exports: [],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot({}),
  ],
  providers: [provideHttpClient(withInterceptorsFromDi()), provideLottieOptions({ player: playerFactory })],
})
export class AppModule {}
