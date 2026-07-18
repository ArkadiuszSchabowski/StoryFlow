import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { StoryService } from 'src/app/_services/story.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
})
export class HomeComponent implements OnInit {
  constructor(
    private storyService: StoryService,
    private router: Router,
  ) {}

  ngOnInit(): void {}

  navigateToWelcomeStory() {
    const WELCOME_STORY_ID: number = 79;
    this.storyService.getWelcomeStory().subscribe({
      next: () => {
        this.router.navigateByUrl(`text/${WELCOME_STORY_ID}`);
      },
      error: (error) => console.log(error),
    });
  }
}
