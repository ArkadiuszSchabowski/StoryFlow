import { Component, OnInit } from '@angular/core';
import { StoryService } from 'src/app/_services/story.service';
import { GetSentenceViewDto } from 'src/app/models/get-sentence-view-dto';
import { GetStoryViewDto } from 'src/app/models/get-story-view-dto';
import { ActivatedRoute, Router } from '@angular/router';
import { GetQuizDto } from 'src/app/models/get-quiz-dto';
import { QuizSubmissionDto } from 'src/app/models/quiz-submission-dto';
import { QuizService } from 'src/app/_services/quiz.service';
import {
  showCategory,
  showLanguageLevel,
  showSize,
} from 'src/app/helpers/formatter';
import { LoaderService } from 'src/app/_services/loader.service';
import { QuizResult } from 'src/app/models/quiz-result';
import { AuthService } from 'src/app/_services/auth.service';
import { NavbarService } from 'src/app/_services/navbar.service';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { LottieComponent } from 'ngx-lottie';
import { AsyncPipe } from '@angular/common';

@Component({
  selector: 'app-text-display',
  templateUrl: './text-display.component.html',
  styleUrls: ['./text-display.component.scss'],
  standalone: true,
  imports: [
    AsyncPipe,
    FormsModule,
    ReactiveFormsModule,
    MatButtonModule,
    MatIconModule,
    LottieComponent,
  ],
})
export class TextDisplayComponent implements OnInit {
  storyId: number = 0;
  story: GetStoryViewDto | null = null;
  isQuiz = false;

  quiz: GetQuizDto | null = null;
  quizSubmission: QuizSubmissionDto | null = {
    storyId: 0,
    answers: [],
  };
  selectedAnswers: { [questionId: number]: number } = {};

  currentQuestionIndex = 0;

  answerSubmitted = false;
  isQuizFinishied = false;
  quizResult: QuizResult | null = null;

  get currentQuestion() {
    return this.quiz?.questions[this.currentQuestionIndex];
  }

  showLanguageLevel = showLanguageLevel;
  showSize = showSize;
  showCategory = showCategory;

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private storyService: StoryService,
    private quizService: QuizService,
    public authService: AuthService,
    public loaderService: LoaderService,
    private navbarService: NavbarService,
  ) {}

  ngOnInit(): void {
    this.showLoader();
    this.getStoryIdFromRoute();
  }

  getStoryIdFromRoute() {
    this.route.paramMap.subscribe((params) => {
      const id = Number(params.get('id'));
      this.storyId = id;
      this.get(id);
    });
  }

  checkAnswer() {
    this.answerSubmitted = true;

    const questionId = this.quiz!.questions[this.currentQuestionIndex].id;
    const answerId = this.selectedAnswers[questionId];
  }

  showLoader() {
    this.route.queryParamMap.subscribe((params) => {
      const showLoader = params.get('showLoader');

      this.authService.currentUserSource$.subscribe((token) => {
        if (!token) {
          this.loaderService.hide();
        }
        if (token && showLoader != 'false') {
          this.loaderService.show();
        }
      });
    });
  }

  showNextQuestion() {
    if (!this.quiz) {
      return;
    }

    if (this.currentQuestionIndex < this.quiz.questions.length - 1) {
      this.currentQuestionIndex++;
      this.answerSubmitted = false;
    } else {
      this.send();
    }
  }

  hideLoader() {
    this.loaderService.hide();
  }

  navigateToTextSelection() {
    this.router.navigateByUrl('/sezon-selection');
  }

  resetState(): void {
    this.loaderService.hide();
    this.isQuiz = false;
    this.quizSubmission = { storyId: 0, answers: [] };
    this.selectedAnswers = {};
    this.currentQuestionIndex = 0;
    this.answerSubmitted = false;
    this.isQuizFinishied = false;
    this.quizResult = null;
  }

  navigateToRegister() {
    this.router.navigateByUrl('/register');
  }

  gotoQuiz() {
    this.isQuiz = true;
  }

  get(id: number) {
    const WELCOME_STORY_ID: number = 79;
    if (this.storyId != 0) {
      if (this.storyId == 79) {
        this.storyService.getWelcomeStory().subscribe({
          next: (response) => {
            this.story = response;
            this.quiz = response.quiz;
          },
          error: (error) => console.log(error),
        });
      }

      if (this.storyId != WELCOME_STORY_ID) {
        this.storyService.get(id).subscribe({
          next: (response) => {
            this.story = response;
            this.quiz = response.quiz;
          },
          error: (error) => {
            if (error.status == 401) {
              this.router.navigateByUrl(`error-page`);
            }
          },
        });
      }
    }
  }

  roundToTwoDecimals(value: number): number {
    return Math.round(value * 100) / 100;
  }

  changeDescriptionLanguage(story: GetStoryViewDto) {
    story.isDescriptionEnglish = !story.isDescriptionEnglish;
  }
  changeSentenceLanguage(sentence: GetSentenceViewDto) {
    sentence.isSentenceEnglish = !sentence.isSentenceEnglish;
  }
  changeTitleLanguage(story: GetStoryViewDto) {
    story.isTitleEnglish = !story.isTitleEnglish;
  }

  send(): void {
    this.isQuizFinishied = true;
    this.navbarService.blockButtons();
    this.loaderService.show();

    const start = Date.now();
    const minTime = 2000;

    if (
      this.storyId != 79 ||
      (this.storyId === 79 && this.authService.getToken())
    ) {
      this.quizSubmission = {
        storyId: this.storyId,
        answers: Object.entries(this.selectedAnswers).map(
          ([questionId, answerId]) => ({
            questionId: Number(questionId),
            answerId,
          }),
        ),
      };
      this.quizService.sendAnswers(this.quizSubmission).subscribe({
        next: (response) => {
          const elapsedTime = Date.now() - start;
          const remaining = Math.max(0, minTime - elapsedTime);

          setTimeout(() => {
            this.quizResult = response;
            this.loaderService.hide();
            this.navbarService.unblockButtons();
          }, remaining);
        },
        error: () => {
          this.loaderService.hide();
          this.navbarService.unblockButtons();
        },
      });
    }

    if (this.storyId === 79 && !this.authService.getToken()) {
      this.quizSubmission = {
        storyId: this.storyId,
        answers: Object.entries(this.selectedAnswers).map(
          ([questionId, answerId]) => ({
            questionId: Number(questionId),
            answerId,
          }),
        ),
      };

      localStorage.setItem('pendingQuiz', JSON.stringify(this.quizSubmission));

      this.quizService
        .sendWelcomeAnswers(this.quizSubmission.answers)
        .subscribe({
          next: (response) => {
            const elapsedTime = Date.now() - start;
            const remaining = Math.max(0, minTime - elapsedTime);
            setTimeout(() => {
              this.quizResult = response;
              this.loaderService.hide();
              this.navbarService.unblockButtons();
            }, remaining);
          },
          error: () => {
            this.loaderService.hide();
            this.navbarService.unblockButtons();
          },
        });

      this.quizSubmission = null;
    }
  }
}
