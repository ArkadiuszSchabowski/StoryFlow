import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { ToastrService } from 'ngx-toastr';
import { QuizService } from 'src/app/_services/quiz.service';
import { StoryService } from 'src/app/_services/story.service';
import { ThemeService } from 'src/app/_services/theme.service';
import { AddQuizDto } from 'src/app/models/add-quiz-dto';
import { GenerateQuizDto } from 'src/app/models/generate-quiz-dto';
import { GenerateQuizResponseDto } from 'src/app/models/generate-quiz-response';
import { GetStoryViewDto } from 'src/app/models/get-story-view-dto';

@Component({
  selector: 'app-quiz-create',
  templateUrl: './quiz-create.component.html',
  styleUrls: ['./quiz-create.component.scss'],
  standalone: true,
  imports: [CommonModule, FormsModule, ReactiveFormsModule, MatButtonModule, MatSelectModule],
})
export class QuizCreateComponent implements OnInit {
  stories: GetStoryViewDto[] = [];
  selectedStory: GetStoryViewDto | null = null;
  storyId: number = 0;
  isResponse = false;
  dto: GenerateQuizDto = {
    englishStory: '',
  };

  generateQuizResponse: GenerateQuizResponseDto | null = null;

  form: any = this.fb.group({
    story: [],
  });

  addForm: any = this.fb.group({
    languageLevel: [''],
  });

  constructor(
    private fb: FormBuilder,
    private storyService: StoryService,
    private quizService: QuizService,
    private toastr: ToastrService,
    public themeService: ThemeService
  ) {}

  ngOnInit(): void {
    this.getAll();
  }

  getAll() {
    this.storyService.getAll(null).subscribe({
      next: (response) => {
        this.stories = response.filter((s) => s.quiz?.id == null);
      },
      error: (error) => {
        this.toastr.error(error.error);
      },
    });
  }

  generateQuiz() {
    this.isResponse = false;

    this.storyId = this.form.value.story;

    this.selectedStory =
      this.stories.find((s) => s.id === this.storyId) ?? null;

    if (!this.selectedStory) {
      return;
    }

    this.dto = {
      englishStory: this.selectedStory.englishStory,
    };

    this.quizService.generate(this.dto).subscribe({
      next: (response) => {
        this.generateQuizResponse = response;
        this.isResponse = true;
      },
      error: (error) => {},
    });
  }

  public add() {
    if (!this.generateQuizResponse) {
      return;
    }

    const dto: AddQuizDto = {
      storyId: this.storyId,
      questions: this.generateQuizResponse.questions.map((q) => ({
        questionText: q.question,
        answers: q.answers.map((answer, index) => ({
          key: String.fromCharCode(65 + index), // A, B, C, D
          text: answer,
          isCorrect: index === q.correctAnswerIndex,
        })),
      })),
    };

    this.quizService.add(dto).subscribe({
      next: () => {
        window.scrollTo(0, 0);
        this.toastr.success('Quiz został dodany.');
        this.addForm.reset();
        this.getAll();
      },
      error: (error) => {},
    });
  }
}
