import { Component } from '@angular/core';
import { FormBuilder, ReactiveFormsModule } from '@angular/forms';
import { MatCardContent, MatCardModule } from '@angular/material/card';
import {MatRadioModule} from '@angular/material/radio';
import { MatFormFieldModule } from "@angular/material/form-field";
import { CommonModule } from '@angular/common';
import { ThemeService } from 'src/app/_services/theme.service';

@Component({
  selector: 'app-personalization',
  imports: [CommonModule, MatCardModule, MatCardContent, ReactiveFormsModule, MatRadioModule, MatFormFieldModule],
  templateUrl: './personalization.component.html',
  styleUrl: './personalization.component.scss',
})
export class PersonalizationComponent {
  form: any = this.fb.group({
    theme: [''],
  });

  isLightMode = false;

  constructor(private fb: FormBuilder, public themeService: ThemeService) {}

  submit(){
    this.isLightMode = !this.isLightMode;
    this.themeService.themeSubject.next(this.isLightMode)
    console.log("submit works!")
  }
}
