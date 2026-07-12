import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { StoryService } from 'src/app/_services/story.service';
import { UserService } from 'src/app/_services/user.service';
import { CATEGORIES_WITH_PLACEHOLDER, SIZES_WITH_PLACEHOLDER, LANGUAGE_LEVELS_WITH_PLACEHOLDER } from 'src/app/constants/select-options';
import { showSize, showLanguageLevelShort, showCategory } from 'src/app/helpers/formatter';
import { GetStoryViewDto } from 'src/app/models/get-story-view-dto';
import { GetUserDto } from 'src/app/models/get-user-dto';
import { StoryFilter } from 'src/app/models/story-filter-dto';

@Component({
  selector: 'app-library-preview',
  templateUrl: './library-preview.component.html',
  styleUrls: ['./library-preview.component.scss']
})
export class LibraryPreviewComponent implements OnInit {
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
    private fb: FormBuilder,
    private storyService: StoryService,
    private router: Router,
    private toastr: ToastrService,
    private userService: UserService,
  ) {}

  ngOnInit(): void {
    this.get();
    this.getProfile();
  }

  get() {
    this.openedStoryId = null;
    this.dto = {
      languageLevel: this.form.value.languageLevel,
      category: this.form.value.category,
      size: this.form.value.size,
    };

    this.storyService.getAll(this.dto).subscribe({
      next: (response) => {
        console.log(response)
        this.stories = response;
      },
      error: () => {},
    });
  }

  getStory(id: number) {
    this.storyService.get(id).subscribe({
      next: () => {
        this.router.navigateByUrl(`text/${id}`);
      },
      error: (error) => {
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