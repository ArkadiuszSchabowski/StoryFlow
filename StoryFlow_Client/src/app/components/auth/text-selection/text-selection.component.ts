import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { StoryService } from 'src/app/_services/story.service';
import { UserService } from 'src/app/_services/user.service';
import { GetStoryViewDto } from 'src/app/models/get-story-view-dto';
import { GetUserDto } from 'src/app/models/get-user-dto';
import { StoryFilter } from 'src/app/models/story-filter-dto';

@Component({
  selector: 'app-text-selection',
  templateUrl: './text-selection.component.html',
  styleUrls: ['./text-selection.component.scss'],
})
export class TextSelectionComponent implements OnInit {
  stories: GetStoryViewDto[] = [];
  selected = 'option2';
  visibleStories: GetStoryViewDto[] = [];
  dto: StoryFilter | null = null;
  icon: string = '';
  openedStoryId: number | null = null;
  profile: GetUserDto | undefined;

  form: any = this.fb.group({
    languageLevel: [],
    category: [],
    size: [],
  });

  categories = [
    { value: null, viewValue: 'Kategoria:' },
    { value: '0', viewValue: 'Zwierzęta' },
    { value: '1', viewValue: 'Zdrowie' },
    { value: '2', viewValue: 'Technologia' },
    { value: '3', viewValue: 'Sport' },
    { value: '4', viewValue: 'Sztuka' },
    { value: '5', viewValue: 'Historia' },
    { value: '6', viewValue: 'Muzyka' },
  ];

  languageLevels = [
    { value: null, viewValue: 'Poziom języka:' },
    { value: '0', viewValue: 'A1 - Początkujący' },
    { value: '1', viewValue: 'A2 - Podstawowy' },
    { value: '2', viewValue: 'B1 - Średnio zaawansowany' },
    { value: '3', viewValue: 'B2 - Wyższy średnio zaawansowany' },
    { value: '4', viewValue: 'C1 - Zaawansowany' },
    { value: '5', viewValue: 'C2 - Biegły' },
  ];

  sizes = [
    { value: null, viewValue: 'Rozmiar historii:' },
    { value: '0', viewValue: 'Krótka' },
    { value: '1', viewValue: 'Średnia' },
    { value: '2', viewValue: 'Długa' },
  ];

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

  getProfile() {
    this.userService.getProfile().subscribe({
      next: (response) => {
        this.profile = response;
        console.log(response);
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
      return 'play_arrow';

    if (userStory?.percentageScore === 100) {
      return 'emoji_events';
    }
    return '';
  }

  hideStoryDescription(storyId: number) {
    if (this.openedStoryId === storyId) {
      this.openedStoryId = null;
    } else {
      this.openedStoryId = storyId;
    }
  }

  toggleStory(storyId: number) {
    if (this.openedStoryId === storyId) {
      this.openedStoryId = null;
    } else {
      this.openedStoryId = storyId;
    }
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

  showLanguageLevelShort(level: any) {
    switch (level) {
      case 0:
        return 'A1';

      case 1:
        return 'A2';

      case 2:
        return 'B1';

      case 3:
        return 'B2';

      case 4:
        return 'C1';

      case 5:
        return 'C2';

      default:
        return level;
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
        return 'Nieznana długość';
    }
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
        this.stories = response;
      },
      error: () => {},
    });
  }

  NavigateToTextDisplay(id: number) {
    this.getStory(id);
  }

  getStory(id: number) {
    this.storyService.get(id).subscribe({
      next: (response) => {
        console.log(response);
        this.router.navigateByUrl(`text/${id}`);
      },
      error: (error) => {
        this.toastr.error(error.error);
      },
    });
  }
}
