import { Component } from '@angular/core';
import { FormBuilder, ReactiveFormsModule } from '@angular/forms';
import { MatCardContent, MatCardModule } from '@angular/material/card';
import { MatRadioModule } from '@angular/material/radio';
import { MatFormFieldModule } from '@angular/material/form-field';
import { CommonModule } from '@angular/common';
import { ThemeService } from 'src/app/_services/theme.service';
import { MatButtonModule } from '@angular/material/button';
import { UserPreferencesService } from 'src/app/_services/user-preferences.service';

@Component({
  selector: 'app-personalization',
  imports: [
    CommonModule,
    MatButtonModule,
    MatCardModule,
    MatCardContent,
    ReactiveFormsModule,
    MatRadioModule,
    MatFormFieldModule,
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
    private userPreferencesService: UserPreferencesService,
  ) {}

  submit() {
    const selectedTheme = this.form.value.theme;

    switch (selectedTheme) {
      case 'darkMode':
        this.isLightMode = false;
        break;
      case 'lightMode':
        this.isLightMode = true;
        break;
    }
    this.themeService.themeSubject.next(this.isLightMode);
    this.userPreferencesService.setTheme(this.isLightMode).subscribe({
      next: (response) => {
        console.log(response);
      },
      error: (error) => this.themeService.themeSubject.next(!this.isLightMode),
    });
  }
}
