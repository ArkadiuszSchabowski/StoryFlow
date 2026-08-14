import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ThemeService } from 'src/app/_services/theme.service';
import { WordLessonService } from 'src/app/_services/word-lesson.service';
import { GetWordLessonDto } from 'src/app/models/get-word-lesson-dto';
import { environment } from 'src/environments/environment';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';

@Component({
  selector: 'app-word-lesson-display',
  imports: [CommonModule, FormsModule, MatFormFieldModule, MatInputModule],
  templateUrl: './word-lesson-display.component.html',
  styleUrl: './word-lesson-display.component.scss',
})
export class WordLessonDisplayComponent implements OnInit {
  wordLessonId: number = 0;
  wordLesson: GetWordLessonDto | null = null;
  apiUrl = environment.apiUrl;
  userAnswer: string = '';
  firstImage: number = 0;
  userScore = 0;

  constructor(
    public themeService: ThemeService,
    private route: ActivatedRoute,
    private wordLessonService: WordLessonService,
    private router: Router,
  ) {}

  ngOnInit(): void {
    this.getStoryIdFromRoute();
  }

  submit(wordLesson: GetWordLessonDto) {

    let userAnswer = this.userAnswer.toLowerCase();
    let correctAnswer = wordLesson!.words![this.firstImage].englishWord!.toLowerCase();
    console.log(userAnswer);
    console.log(correctAnswer);
    if (userAnswer === correctAnswer) {
      this.userScore += 10;
      this.wordLessonService.save(this.wordLessonId, this.userScore).subscribe({
        next: response => console.log(response),
        error: error => console.log(error)
      })
      if(this.userScore === 120){
        return;
      }
      this.firstImage++;
    }
  }

  getStoryIdFromRoute() {
    this.route.paramMap.subscribe((params) => {
      const id = Number(params.get('id'));
      this.wordLessonId = id;
      this.get(id);
    });
  }
  getSentenceParts(hintSentence: string | null): string[] {
    if (hintSentence !== null) {
      return hintSentence.split('{{blank}}');
    }
    return [];
  }

  get(id: number) {
    this.wordLessonService.get(id).subscribe({
      next: (response) => {
        this.wordLesson = response;
        console.log(this.wordLesson);
      },
      error: (error) => {
        if (error.status == 401) {
          this.router.navigateByUrl(`error-page`);
        }
        if (error.status === 400) {
          this.router.navigateByUrl('sezon-selection');
        }
        if (error.status == 404) {
          this.router.navigateByUrl(`sezon-selection`);
        }
      },
    });
  }
}
