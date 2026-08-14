import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { ActivatedRoute, Router } from '@angular/router';
import { LottieComponent } from 'ngx-lottie';
import { ToastrService } from 'ngx-toastr';
import { LoaderService } from 'src/app/_services/loader.service';
import { SeasonService } from 'src/app/_services/season.service';
import { StoryService } from 'src/app/_services/story.service';
import { ThemeService } from 'src/app/_services/theme.service';
import { UserService } from 'src/app/_services/user.service';
import {
  CATEGORIES_WITH_PLACEHOLDER,
  SIZES_WITH_PLACEHOLDER,
  LANGUAGE_LEVELS_WITH_PLACEHOLDER,
} from 'src/app/constants/select-options';
import {
  showSize,
  showLanguageLevelShort,
  showCategory,
} from 'src/app/helpers/formatter';
import { GetStorySeasonDto } from 'src/app/models/get-story-season-dto';
import { GetStoryViewDto } from 'src/app/models/get-story-view-dto';
import { GetUserDto } from 'src/app/models/get-user-dto';
import { GetWordLessonDto } from 'src/app/models/get-word-lesson-dto';
import { StoryFilter } from 'src/app/models/story-filter-dto';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-sezon',
  templateUrl: './sezon.component.html',
  styleUrls: ['./sezon.component.scss'],
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    MatButtonModule,
    MatIconModule,
    LottieComponent,
  ],
})
export class SezonComponent implements OnInit {
  apiUrl = environment.apiUrl;
  seasonId: number = 0;
  wordLessons: GetWordLessonDto[] = [];
  storyLessons: GetStoryViewDto[] = [];
  seasonResponse: GetStorySeasonDto | null = null;
  dto: StoryFilter | null = null;
  icon: string = '';
  openedStoryId: number | null = null;
  openedWordLessonId: number | null = null;
  profile: GetUserDto | undefined;

  categories = CATEGORIES_WITH_PLACEHOLDER;
  sizes = SIZES_WITH_PLACEHOLDER;
  languageLevels = LANGUAGE_LEVELS_WITH_PLACEHOLDER;

  showSize = showSize;
  showLanguageLevelShort = showLanguageLevelShort;
  showCategory = showCategory;

  form: any = this.fb.group({
    languageLevel: [],
    category: [],
    size: [],
  });

  constructor(
    private route: ActivatedRoute,
    private fb: FormBuilder,
    private seasonService: SeasonService,
    private router: Router,
    private toastr: ToastrService,
    private userService: UserService,
    private storyService: StoryService,
    public loaderService: LoaderService,
    public themeService: ThemeService,
  ) {}

  ngOnInit(): void {
    this.getIdFromRoute();
    this.getProfile();
  }
  getIdFromRoute() {
    this.route.paramMap.subscribe((params) => {
      const id = Number(params.get('id'));
      this.seasonId = id;
      this.getBySeason(id);
    });
  }

  getBySeason(seasonId: number) {
    this.seasonService.getBySeason(seasonId).subscribe({
      next: (response) => {
        this.seasonResponse = response;
        console.log(this.seasonResponse);
        this.wordLessons = response.wordLessons;
        console.log(this.wordLessons);
        this.storyLessons = response.stories;
        console.log(this.storyLessons);
      },
      error: (error) => {
        console.log(error);
      },
    });
  }

  getStory(id: number) {
    this.storyService.get(id).subscribe({
      next: () => {
        this.router.navigateByUrl(`text/${id}`);
      },
      error: (error) => {
        this.loaderService.hide();
        this.toastr.error(error.error);
      },
    });
  }

  getProfile() {
    this.userService.getProfile().subscribe({
      next: (response) => {
        this.profile = response;
      },
      error: () => {},
    });
  }

    getIconForWordLesson(wordLessonId: number): string {

    const userWordLesson = this.wordLessons
      .flatMap((wl) => wl.userWordLessons ?? [])
      .find((uwl) => uwl.wordLessonId === wordLessonId);


    if (userWordLesson == null && this.profile?.tickets == 0) {
      return 'lock';
    }
    if (userWordLesson == null) {
      return 'play_arrow';
    }

    if (userWordLesson?.percentageScore < 50) {
      return 'replay';
    }
    if (userWordLesson?.percentageScore >= 50 && userWordLesson?.percentageScore < 100)
      return 'thumb_up';

    if (userWordLesson?.percentageScore === 100) {
      return 'emoji_events';
    }
    return '';
  }

  getIconForStory(storyId: number): string {
    const userStory = this.storyLessons
      .flatMap((s) => s.userStories ?? [])
      .find((us) => us.storyId === storyId);

    if (userStory == null && this.profile?.tickets == 0) {
      return 'lock';
    }
    if (userStory == null) {
      return 'play_arrow';
    }

    if (userStory?.percentageScore < 50) {
      return 'replay';
    }
    if (userStory?.percentageScore >= 50 && userStory?.percentageScore < 100)
      return 'thumb_up';

    if (userStory?.percentageScore === 100) {
      return 'emoji_events';
    }
    return '';
  }

  toggleStory(storyId: number) {
    this.openedWordLessonId = null;
    if (this.openedStoryId === storyId) {
      this.openedStoryId = null;
    } else {
      this.openedStoryId = storyId;
    }
  }

  toggleWordLesson(wordLessonId: number) {
    this.openedStoryId = null;
    if (this.openedWordLessonId === wordLessonId) {
      this.openedWordLessonId = null;
    } else {
      this.openedWordLessonId = wordLessonId;
    }
  }

  roundToTwoDecimals(value: number): number {
    return Math.round(value * 100) / 100;
  }

  NavigateToTextDisplay(id: number) {
    this.getStory(id);
  }

  navigateToWordLessonDisplay(id: number) {
    this.router.navigateByUrl(`word-lesson/${id}`);
  }
}
