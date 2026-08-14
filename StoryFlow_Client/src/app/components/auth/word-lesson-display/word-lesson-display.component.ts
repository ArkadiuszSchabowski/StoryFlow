import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { ThemeService } from 'src/app/_services/theme.service';

@Component({
  selector: 'app-word-lesson-display',
  imports: [CommonModule],
  templateUrl: './word-lesson-display.component.html',
  styleUrl: './word-lesson-display.component.scss',
})
export class WordLessonDisplayComponent {
constructor(public themeService: ThemeService){}
}
