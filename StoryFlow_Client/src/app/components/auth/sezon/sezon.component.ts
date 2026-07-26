import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { LoaderService } from 'src/app/_services/loader.service';
import { StoryService } from 'src/app/_services/story.service';
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
import { GetStoryViewDto } from 'src/app/models/get-story-view-dto';
import { GetUserDto } from 'src/app/models/get-user-dto';
import { StoryFilter } from 'src/app/models/story-filter-dto';

@Component({
  selector: 'app-sezon',
  templateUrl: './sezon.component.html',
  styleUrls: ['./sezon.component.scss'],
  standalone: true,
  imports: [FormsModule, ReactiveFormsModule, MatButtonModule, MatIconModule],
})
export class SezonComponent implements OnInit {
  seasonId: number = 0;
  stories: GetStoryViewDto[] = [];
  dto: StoryFilter | null = null;
  icon: string = '';
  openedStoryId: number | null = null;
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
    private storyService: StoryService,
    private router: Router,
    private toastr: ToastrService,
    private userService: UserService,
    private loaderService: LoaderService,
  ) {}

  ngOnInit(): void {
    this.getIdFromRoute();
    this.getProfile();
  }
  getIdFromRoute() {
    this.route.paramMap.subscribe((params) => {
      const id = Number(params.get('id'));
      this.seasonId = id;
      this.get(id);
    });
  }

  get(seasonId: number) {
    this.openedStoryId = null;
    this.dto = {
      languageLevel: this.form.value.languageLevel,
      category: this.form.value.category,
      size: this.form.value.size,
    };

    this.apiCallGetBySeason(seasonId);
  }

  apiCallGetBySeason(seasonId: number) {
    this.storyService.getBySeason(seasonId).subscribe({
      next: (response) => {
        this.stories = response;
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

  getIcon(storyId: number): string {
    const userStory = this.stories
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
    if (this.openedStoryId === storyId) {
      this.openedStoryId = null;
    } else {
      this.openedStoryId = storyId;
    }
  }

  roundToTwoDecimals(value: number): number {
    return Math.round(value * 100) / 100;
  }

  NavigateToTextDisplay(id: number) {
    this.getStory(id);
  }
}
