import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NavbarComponent } from './components/shared/navbar/navbar.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ErrorPageComponent } from './components/system/error-page/error-page.component';
import { ToastrModule } from 'ngx-toastr';
import { MaterialModule } from './modules/material/material.module';
import { PublicModule } from './modules/public/public.module';
import { AuthModule } from './modules/auth/auth.module';
import { SezonSelectionComponent } from './components/auth/sezon-selection/sezon-selection.component';
import { SezonComponent } from './components/auth/sezon/sezon.component';

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    ErrorPageComponent,
    SezonSelectionComponent,
    SezonComponent,
  ],
  imports: [
    AuthModule,
    BrowserModule,
    MaterialModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    FormsModule,
    PublicModule,
    ReactiveFormsModule,
    ToastrModule.forRoot({}),
  ],
  exports: [

  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
