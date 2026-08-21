import { CommonModule } from '@angular/common';
import {
  AfterViewInit,
  Component,
  ElementRef,
  OnInit,
  ViewChild,
} from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ThemeService } from 'src/app/_services/theme.service';
import { WordLessonService } from 'src/app/_services/word-lesson.service';
import { GetWordLessonDto } from 'src/app/models/get-word-lesson-dto';
import { environment } from 'src/environments/environment';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { AddWordResultDto } from 'src/app/models/add-word-lesson-dto';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatIconModule } from '@angular/material/icon';

@Component({
  selector: 'app-word-lesson-display',
  imports: [
    CommonModule,
    FormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatProgressBarModule,
    MatIconModule,
  ],
  templateUrl: './word-lesson-display.component.html',
  styleUrl: './word-lesson-display.component.scss',
})
export class WordLessonDisplayComponent implements OnInit, AfterViewInit {
  @ViewChild('answerInput') answerInput!: ElementRef<HTMLInputElement>;
  wordLessonId: number = 0;
  wordLesson: GetWordLessonDto | null = null;
  apiUrl = environment.apiUrl;
  userAnswer: string = '';
  firstImage: number = 0;
  dto: AddWordResultDto = {
    correctAnswersCount: 0,
    result: 0,
  };
  userScore = 0;
  quizProgress: number = 8.33;
  isLessonFinished: boolean = false;
  percentageScore = 100;
  isCorrectAnswerShow = false;
  correctAnswer = '';
  isWaitingAfterWrongAnswer = false;
  totalWordsCount = 0;

  constructor(
    public themeService: ThemeService,
    private route: ActivatedRoute,
    private wordLessonService: WordLessonService,
    private router: Router,
  ) {}

  ngAfterViewInit(): void {
    this.focusInput();
  }

  ngOnInit(): void {
    this.getStoryIdFromRoute();
  }

  private focusInput(): void {
    setTimeout(() => this.answerInput?.nativeElement.focus(), 0);
  }

  roundToTwoDecimals(value: number): number {
    return Math.round(value * 100) / 100;
  }

  submit(wordLesson: GetWordLessonDto) {
    if (window.innerWidth < 768) {
      window.scrollTo(0, 56);
    }
    if (this.isWaitingAfterWrongAnswer) {
      return;
    }

    const userAnswer = this.userAnswer.toLowerCase().trim();
    this.correctAnswer = wordLesson.words[this.firstImage]
      .englishWord!.toLowerCase()
      .trim();

    if (userAnswer !== this.correctAnswer) {
      this.isCorrectAnswerShow = true;
      this.isWaitingAfterWrongAnswer = true;

      setTimeout(() => {
        const wrongWord = wordLesson.words.splice(this.firstImage, 1)[0];
        wordLesson.words.push(wrongWord);

        this.isCorrectAnswerShow = false;
        this.correctAnswer = '';
        this.userAnswer = '';
        this.isWaitingAfterWrongAnswer = false;
        this.focusInput();
      }, 2500);

      return;
    }

    this.correctAnswer = '';
    this.dto.correctAnswersCount++;
    this.dto.result += 10;
    this.quizProgress += 100 / this.totalWordsCount;
    this.userAnswer = '';

    this.wordLessonService.save(this.wordLessonId, this.dto).subscribe({
      next: (response) => {},
      error: (error) => console.log(error),
    });

    wordLesson.words.splice(this.firstImage, 1);

    if (wordLesson.words.length === 0) {
      this.isLessonFinished = true;
      return;
    }

    this.focusInput();
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

  navigateToTextSelection() {
    this.router.navigateByUrl('/sezon-selection');
  }

  resetState(): void {
    this.dto.correctAnswersCount = 0;
    this.dto.result = 0;
    this.isLessonFinished = false;
    this.quizProgress = 8.33;
    this.firstImage = 0;
    this.isWaitingAfterWrongAnswer = false;
    this.correctAnswer = '';
    this.get(this.wordLessonId);
  }

  get(id: number) {
    this.wordLessonService.get(id).subscribe({
      next: (response) => {
        if (window.innerWidth < 768) {
          window.scrollTo(0, 56);
        }
        this.wordLesson = response;
        this.totalWordsCount = response.words.length;
        this.focusInput();
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
