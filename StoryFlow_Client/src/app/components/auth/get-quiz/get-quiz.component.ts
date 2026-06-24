import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { StoryService } from 'src/app/_services/story.service';
import { GetQuizDto } from 'src/app/models/get-quiz-dto';
import { GetStoryViewDto } from 'src/app/models/get-story-view-dto';

@Component({
  selector: 'app-get-quiz',
  templateUrl: './get-quiz.component.html',
  styleUrls: ['./get-quiz.component.scss'],
})
export class GetQuizComponent implements OnInit {
  storyId: number = 0;
  story: GetStoryViewDto | null = null;
  quiz: GetQuizDto | null = null;

  selectedAnswers: { [questionId: number]: number } = {};

  submitQuiz(): void {
    console.log(this.selectedAnswers);
  }
  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private storyService: StoryService,
  ) {}

  ngOnInit(): void {
    this.route.paramMap.subscribe((params) => {
      const id = Number(params.get('id'));
      this.storyId = id;
      this.get(id);
    });
  }
  get(id: number) {
    if (this.storyId != 0) {
      this.storyService.get(id).subscribe({
        next: (response) => {
          this.quiz = response.quiz;
          this.story = response;
        },
        error: () => this.router.navigateByUrl(`error-page`),
      });
    }
  }
}
