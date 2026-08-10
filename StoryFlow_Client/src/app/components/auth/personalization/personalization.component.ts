import { Component } from '@angular/core';
import { FormBuilder, ReactiveFormsModule } from '@angular/forms';
import { MatCardContent, MatCardModule } from '@angular/material/card';
import { MatRadioModule } from '@angular/material/radio';
import { MatFormFieldModule } from '@angular/material/form-field';
import { CommonModule } from '@angular/common';
import { ThemeService } from 'src/app/_services/theme.service';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-personalization',
  imports: [
    CommonModule,
    MatButtonModule,
    MatCardModule,
    MatCardContent,
    ReactiveFormsModule,
    MatRadioModule,
    MatFormFieldModule
  ],
  templateUrl: './personalization.component.html',
  styleUrl: './personalization.component.scss',
})
export class PersonalizationComponent {
  form = this.fb.group({
    theme: [this.themeService.themeSubject.value ? 'lightMode' : 'darkMode'],
  });

isLightMode = true;

  constructor(
    private fb: FormBuilder,
    public themeService: ThemeService,
  ) {}

  submit() {
    const selectedTheme = this.form.value.theme;

    switch (selectedTheme) {
      case 'darkMode':
        this.isLightMode = false;
        break;
      case 'lightMode':
        this.isLightMode = true;
    }
    this.themeService.themeSubject.next(this.isLightMode);
  }
}
