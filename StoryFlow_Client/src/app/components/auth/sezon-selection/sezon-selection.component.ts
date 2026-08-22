import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { Router } from '@angular/router';
import { LottieComponent } from 'ngx-lottie';
import { LoaderService } from 'src/app/_services/loader.service';
import { StoryService } from 'src/app/_services/story.service';
import { ThemeService } from 'src/app/_services/theme.service';
import { UserService } from 'src/app/_services/user.service';
import { GetStorySeasonDto } from 'src/app/models/get-story-season-dto';
import { GetStoryViewDto } from 'src/app/models/get-story-view-dto';
import { GetUserDto } from 'src/app/models/get-user-dto';

@Component({
  selector: 'app-sezon-selection',
  templateUrl: './sezon-selection.component.html',
  styleUrls: ['./sezon-selection.component.scss'],
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    MatButtonModule,
    LottieComponent,
    MatIconModule,
  ],
})
export class SezonSelectionComponent implements OnInit {
  seasons: GetStorySeasonDto[] = [];
  stories: GetStoryViewDto[] = [];
  profile: GetUserDto | undefined;

  constructor(
    private storyService: StoryService,
    private router: Router,
    private userService: UserService,
    public themeService: ThemeService,
    public loaderService: LoaderService,
  ) {}

  ngOnInit(): void {
    this.getSeasons();
    this.getProfile();
  }

  getProfile() {
    this.userService.getProfile().subscribe({
      next: (response) => {
        this.profile = response;
      },
      error: () => {},
    });
  }

  goToSeason(id: number) {
    this.router.navigateByUrl(`/sezon/${id}`);
  }

  countUserResult(season: GetStorySeasonDto): number {
    let storySum = 0;
    season.stories.forEach((story) => {
      story.userStories.forEach((us) => {
        storySum += us.bestResult;
      });
    });

    let wordLessonSum = 0;
    season.wordLessons.forEach((wordLesson) => {
      wordLesson.userWordLessons.forEach((uwl) => {
        wordLessonSum += uwl.bestResult;
      });
    });

    return storySum + wordLessonSum;
  }

  getSeasons() {
    this.storyService.getSeasons().subscribe({
      next: (response) => {
        this.seasons = response;
        
      },
      error: (error) => console.log(error),
    });
  }
}
