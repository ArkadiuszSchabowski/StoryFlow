import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { StoryService } from 'src/app/_services/story.service';
import { GetStoryViewDto } from 'src/app/models/get-story-view-dto';
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

  form: any = this.fb.group({
    languageLevel: [],
    category: [],
    size: [],
  });

  categories = [
    { value: null, viewValue: 'Category:' },
    { value: '0', viewValue: 'Animals' },
    { value: '1', viewValue: 'Health' },
    { value: '2', viewValue: 'Technology' },
    { value: '3', viewValue: 'Sport' },
    { value: '4', viewValue: 'Art' },
    { value: '5', viewValue: 'History' },
    { value: '6', viewValue: 'Music' },
  ];

  languageLevels = [
    { value: null, viewValue: 'Language level:' },
    { value: '0', viewValue: 'A1 - Begginer' },
    { value: '1', viewValue: 'A2 - Elementary' },
    { value: '2', viewValue: 'B1 - Intermediate' },
    { value: '3', viewValue: 'B2 - Upper-Intermediate' },
    { value: '4', viewValue: 'C1 - Advanced' },
    { value: '5', viewValue: 'C2 - Proficiency' },
  ];

  sizes = [
    { value: null, viewValue: 'Story size:' },
    { value: '0', viewValue: 'Short' },
    { value: '1', viewValue: 'Medium' },
    { value: '2', viewValue: 'Long' },
  ];

  dto: StoryFilter | null = null;

  constructor(
    private fb: FormBuilder,
    private storyService: StoryService,
    private router: Router,
  ) {}

  ngOnInit(): void {
    this.get();
  }

  showLanguageLevel(level: any) {
    switch (level) {
      case 0:
        return 'A1 - Beginner';
      case 1:
        return 'A2 - Elementary';
      case 2:
        return 'B1 - Intermediate';
      case 3:
        return 'B2 - Upper-Intermediate';
      case 4:
        return 'C1 - Advanced';
      case 5:
        return 'C2 - Proficiency';
      default:
        return 'Unknown';
    }
  }

  showSize(size: any) {
    switch (size) {
      case 0:
        return 'Short';
      case 1:
        return 'Medium';
      case 2:
        return 'Long';
      default:
        return 'Unknown';
    }
  }

  get() {
    this.dto = {
      languageLevel: this.form.value.languageLevel,
      category: this.form.value.category,
      size: this.form.value.size,
    };

    this.storyService.getAll(this.dto).subscribe({
      next: (response) => {
        console.log(response);
        this.stories = response;
      },
      error: (error) => console.log(error),
    });
  }

  NavigateToTextDisplay(id: number | null) {
    this.router.navigateByUrl(`text/${id}`);
  }
}
