import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomeComponent } from 'src/app/components/public/home/home.component';
import { LoginComponent } from 'src/app/components/public/login/login.component';
import { RegisterComponent } from 'src/app/components/public/register/register.component';
import { MaterialModule } from '../material/material.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { playerFactory } from 'src/app/config/lottie.config';
import { LottieComponent, provideLottieOptions } from 'ngx-lottie';
import { ErrorPageComponent } from 'src/app/components/system/error-page/error-page.component';

@NgModule({
  declarations: [HomeComponent, LoginComponent, RegisterComponent, ErrorPageComponent],
  imports: [CommonModule, MaterialModule, FormsModule, ReactiveFormsModule, LottieComponent,],
  exports: [HomeComponent, LoginComponent, RegisterComponent, ErrorPageComponent],
  providers: [provideLottieOptions({ player: playerFactory })],
})
export class PublicModule {}
