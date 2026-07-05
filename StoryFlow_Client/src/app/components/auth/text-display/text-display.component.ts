import { Component, OnInit } from '@angular/core';
import { StoryService } from 'src/app/_services/story.service';
import { GetSentenceViewDto } from 'src/app/models/get-sentence-view-dto';
import { GetStoryViewDto } from 'src/app/models/get-story-view-dto';
import { ActivatedRoute, Router } from '@angular/router';
import { GetQuizDto } from 'src/app/models/get-quiz-dto';
import { QuizSubmissionDto } from 'src/app/models/quiz-submission-dto';
import { QuizService } from 'src/app/_services/quiz.service';

@Component({
  selector: 'app-text-display',
  templateUrl: './text-display.component.html',
  styleUrls: ['./text-display.component.scss'],
})
export class TextDisplayComponent implements OnInit {
  storyId: number = 0;
  story: GetStoryViewDto | null = null;
  isQuiz = false;

  quiz: GetQuizDto | null = null;
  quizSubmission: QuizSubmissionDto = {
    storyId: 0,
    answers: [],
  };
  selectedAnswers: { [questionId: number]: number } = {};

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private storyService: StoryService,
    private quizService: QuizService,
  ) {}

  ngOnInit(): void {
    this.route.paramMap.subscribe((params) => {
      const id = Number(params.get('id'));
      this.storyId = id;
      this.get(id);
    });
  }
  gotoQuiz() {
    this.isQuiz = true;
  }

  get(id: number) {
    if (this.storyId != 0) {
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

  changeDescriptionLanguage(story: GetStoryViewDto) {
    story.isDescriptionEnglish = !story.isDescriptionEnglish;
  }
  changeSentenceLanguage(sentence: GetSentenceViewDto) {
    sentence.isSentenceEnglish = !sentence.isSentenceEnglish;
  }
  changeTitleLanguage(story: GetStoryViewDto) {
    story.isTitleEnglish = !story.isTitleEnglish;
  }
showLanguageLevel(level: any) {
  switch (level) {
    case 0:
      return 'A1 - Początkujący';
    case 1:
      return 'A2 - Podstawowy';
    case 2:
      return 'B1 - Średnio zaawansowany';
    case 3:
      return 'B2 - Wyższy średnio zaawansowany';
    case 4:
      return 'C1 - Zaawansowany';
    case 5:
      return 'C2 - Biegły';
    default:
      return 'Nieznany';
  }
}

showSize(size: any) {
  switch (size) {
    case 0:
      return 'Krótka';
    case 1:
      return 'Średnia';
    case 2:
      return 'Długa';
    default:
      return 'Nieznana długość historii';
  }
}

showCategory(category: any) {
  switch (category) {
    case 0:
      return 'Zwierzęta';
    case 1:
      return 'Zdrowie';
    case 2:
      return 'Technologia';
    case 3:
      return 'Sport';
    case 4:
      return 'Sztuka';
    case 5:
      return 'Historia';
    case 6:
      return 'Muzyka';
    default:
      return 'Nieznany';
  }
}

  send(): void {
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
      next: (response) => console.log(response),
      error: (error) => console.log(error),
    });
  }
}
